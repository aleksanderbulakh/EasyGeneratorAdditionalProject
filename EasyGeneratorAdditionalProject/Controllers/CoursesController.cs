using EasyGeneratorAdditionalProject.Database.Context;
using EasyGeneratorAdditionalProject.Database.Entities;
using EasyGeneratorAdditionalProject.Database.Interfaces;
using EasyGeneratorAdditionalProject.Database.Managers;
using EasyGeneratorAdditionalProject.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesDataProvider _courseProvider;
        private readonly IUsersDataProvider _userProvider;
        private readonly ISectionsDataProvider _sectionPtovider;
        private readonly IContentDataProvider _contentProvider;
        private readonly IMaterialsDataProvider _materialProvider;
        private readonly ISingleSelectQuestionsDataProvider _SSQProvider;
        private readonly IMultipleSelectQuestionDataProvider _MSQProvider;
        private readonly ISingleSelectImageQuestionsDataProvider _SSIQProvider;
        public CoursesController(ICoursesDataProvider courseProvider, IUsersDataProvider userProvider,
            ISectionsDataProvider sectionProvider, IContentDataProvider contentProvideer,
            IMaterialsDataProvider materialProvider, ISingleSelectQuestionsDataProvider SSQProvider,
            IMultipleSelectQuestionDataProvider MSQProvider, ISingleSelectImageQuestionsDataProvider SSIQProvider)
        {
            _courseProvider = courseProvider;
            _userProvider = userProvider;
            _sectionPtovider = sectionProvider;
            _contentProvider = contentProvideer;
            _materialProvider = materialProvider;
            _SSQProvider = SSQProvider;
            _MSQProvider = MSQProvider;
            _SSIQProvider = SSIQProvider;
        }

        [Route("courses", Name = "coursesList")]
        public JsonResult CoursesList()
        {
            var db = new DatabaseContext();

            var userId = db.Users.ToList()[0].Id;
            var data = courseListWrapper(userId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private List<CourseModel> courseListWrapper(Guid id)
        {
            using (var userManager = new UsersManager(_userProvider))
            {
                using (var courseManager = new CoursesManager(_courseProvider))
                {
                    using (var sectionManager = new SectionManager(_sectionPtovider))
                    {
                        using (var contentManager = new ContentManager(_contentProvider, _materialProvider, _SSQProvider, _MSQProvider, _SSIQProvider))
                        {
                            var courseList = courseManager.GetCoursesByUserId(id).ToList();

                            var result = new List<CourseModel>();

                            foreach (var courseObj in courseList)
                            {
                                var courseSections = sectionManager.GetSectionsByCourseId(courseObj.Id);
                                var sectionList = new List<SectionModel>();

                                foreach (var sectionObj in courseSections)
                                {
                                    var sectionContent = contentManager.GetContentBySectionId(sectionObj.Id);

                                    var sectionModel = new SectionModel
                                    {
                                        Id = sectionObj.Id,
                                        Title = sectionObj.Title,
                                        CreatedBy = sectionObj.CreatedBy,
                                        CreatedOn = sectionObj.CreatedOn.ToShortDateString(),
                                        LastModifiedDate = sectionObj.LastModifiedDate.ToShortDateString(),
                                        ContentList = sectionContent
                                    };

                                    sectionList.Add(sectionModel);
                                }

                                var courseModel = new CourseModel
                                {
                                    Id = courseObj.Id,
                                    Title = courseObj.Title,
                                    Description = courseObj.Description,
                                    CreatedBy = userManager.GetUserById(courseObj.UserId).UserName,
                                    CreatedOn = courseObj.CreatedOn.ToShortDateString(),
                                    LastModifiedDate = courseObj.LastModifiedDate.ToShortDateString(),
                                    SectionsList = sectionList
                                };

                                result.Add(courseModel);
                            }

                            return result;
                        }
                    }
                }
            }
        }
    }
}