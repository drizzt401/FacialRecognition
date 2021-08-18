using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FacialRecognition.Data.Models;
using FacialRecognition.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;

namespace FacialRecognition.UI.Pages
{
    public class ValidateStudentModel : PageModel
    {
        private readonly IFacialRecognitionService facialRecognitionService;
        private readonly UserManager<AppUser> userManager;
        private readonly IWebHostEnvironment environment;
        private static string SUBSCRIPTION_KEY = string.Empty;
        private static string ENDPOINT = string.Empty;
        private static string IMAGE_BASE_URL = string.Empty;
        private static string sourceFileName = string.Empty;
        private static string targetFileName = string.Empty;
        const string RECOGNITION_MODEL4 = RecognitionModel.Recognition04;
        IFaceClient client = Authenticate(ENDPOINT, SUBSCRIPTION_KEY);
        public ValidateStudentModel(IFacialRecognitionService _facialRecognitionService, UserManager<AppUser> _userManager, IConfiguration _config, IWebHostEnvironment _environment)
        {
            facialRecognitionService = _facialRecognitionService;
            userManager = _userManager;
            environment = _environment;
            SUBSCRIPTION_KEY = _config.GetValue<string>("SUBSCRIPTION_KEY");
            ENDPOINT = _config.GetValue<string>("ENDPOINT");
        }

        [BindProperty(SupportsGet = true)]
        public List<Student> Students { get; set; }
        public List<SelectListItem> CourseOptions { get; set; }


        public class StudentModel
        {
            public string picture { get; set; }
            public string studentId{ get; set; }
        }
        public void OnGet()
        {
            var user = userManager.GetUserAsync(User);
            CourseOptions = facialRecognitionService.GetCoursesByLecturer(user.Result.Id).Select(a => new SelectListItem
            {
                Value = a.CourseCode.ToString(),
                Text = $"{a.CourseCode}:  {a.CourseTitle}"
            }).ToList();
        }

        public async Task<IActionResult> OnGetGetCourseStudents(string courseCode)
        {
            Course course = facialRecognitionService.GetCourses().Where(c => c.CourseCode == courseCode).SingleOrDefault();
            var students = facialRecognitionService.GetCourseStudents(course);
            List<Datum> data = new List<Datum>();
            AjaxResponse response = new AjaxResponse();
            foreach (Student student in students)
            {
                data.Add(new Datum
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    RegNo = student.RegistrationNumber,
                    Department = student.Department.Name
                });
            }

            var str = JsonConvert.SerializeObject(data);
            var res = JsonConvert.DeserializeObject<Datum[]>(str);
            response.data = res;
            return new JsonResult(response.data);
        }

        public async Task<IActionResult> OnPostUploadImage( StudentModel model)
        {
            try
            {
                string res = string.Empty;
                if (model.picture != null)
                {
                    if (model.picture.Length > 0)
                    {

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var fileExtension = ".jpeg";

                        // concatenating  FileName + FileExtension
                        var newFileName = String.Concat(myUniqueFileName, fileExtension);

                        // Combines two strings into a path.
                        string filepath = Path.Combine(environment.WebRootPath, @$"img\{newFileName}" );

                        byte[] bytes = Convert.FromBase64String(model.picture);
                        System.IO.File.WriteAllBytes(filepath, bytes);
                        IMAGE_BASE_URL = Path.Combine(environment.WebRootPath, @"img\" );
                        targetFileName = newFileName;
                        sourceFileName = GetStudentImage(model.studentId);
                        var similarResult = await FindSimilar(client, IMAGE_BASE_URL, sourceFileName, targetFileName, RECOGNITION_MODEL4);
                        if (similarResult > 0.6)
                        {
                            res = "This student has been registered for this course";
                        }
                        else res = "The image does not match our records in the database";
                    }
                }


                return new JsonResult(res);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private string GetStudentImage(string studentId)
        {
            var myUniqueFileName = Convert.ToString(Guid.NewGuid());
            var fileExtension = ".jpeg";
            var newFileName = String.Concat(myUniqueFileName, fileExtension);
            string filepath = Path.Combine(environment.WebRootPath, @$"img\{newFileName}");
            var image = Convert.ToBase64String(facialRecognitionService.GetStudentImage(studentId));
            byte[] imagebytes = Convert.FromBase64String(image);
            System.IO.File.WriteAllBytes(filepath, imagebytes);
            return newFileName;

        }

        public static IFaceClient Authenticate(string endpoint, string key)
        {
            return new FaceClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };
        }

            /*
     * FIND SIMILAR
     * This example will take an image and find a similar one to it in another image.
     */
        public static async Task<double> FindSimilar(IFaceClient client, string url, string sourceImageFileName, string targetImageFileName, string recognition_model)
        {
            double confidence = 0;
            IList<Guid?> targetFaceIds = new List<Guid?>();
                // Detect faces from target image url.
                var faces = await DetectFaceRecognize(client, $"{url}{targetFileName}", recognition_model);
                // Add detected faceId to list of GUIDs.
                targetFaceIds.Add(faces[0].FaceId.Value);

            // Detect faces from source image url.
            IList<DetectedFace> detectedFaces = await DetectFaceRecognize(client, $"{url}{sourceFileName}", recognition_model);
            Debug.WriteLine(detectedFaces[0].FaceId.Value);

            // Find a similar face(s) in the list of IDs. Comapring only the first in list for testing purposes.
            IList<SimilarFace> similarResults = await client.Face.FindSimilarAsync(detectedFaces[0].FaceId.Value, null, null, targetFaceIds);
            foreach (var similarResult in similarResults)
            {
                Debug.WriteLine($"Faces from {sourceImageFileName} & ID:{similarResult.FaceId} are similar with confidence: {similarResult.Confidence}.");

                confidence = similarResult.Confidence;
            }
            return confidence;
        }
        private static async Task<List<DetectedFace>> DetectFaceRecognize(IFaceClient faceClient, string url, string recognition_model)
        {

            List<DetectedFace> detectedFaces = new List<DetectedFace>() ;
            byte[] file = System.IO.File.ReadAllBytes(url);
            MemoryStream fs = new MemoryStream(file);
            try
            {

                // Detect faces from image URL. Since only recognizing, use the recognition model 1.
                // We use detection model 3 because we are not retrieving attributes.
                //IList<DetectedFace> detectedFaces = await faceClient.Face.DetectWithUrlAsync(url, recognitionModel: recognition_model, detectionModel: DetectionModel.Detection03);
                IList<DetectedFace> detectedFaces_ = await faceClient.Face.DetectWithStreamAsync(fs, recognitionModel: recognition_model, detectionModel: DetectionModel.Detection03);
                Debug.WriteLine($"{detectedFaces_.Count} face(s) detected from image `{Path.GetFileName(url)}`");
                detectedFaces = detectedFaces_.ToList();
            }
            catch(Exception e)
            {
                return null;

            }
            finally
            {

                fs.Close();
            }

            return detectedFaces;
        }
    }
}
