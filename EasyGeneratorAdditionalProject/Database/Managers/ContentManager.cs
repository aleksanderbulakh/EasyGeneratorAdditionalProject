using EasyGeneratorAdditionalProject.Database.Entities;
using EasyGeneratorAdditionalProject.Database.Interfaces;
using EasyGeneratorAdditionalProject.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Managers
{
    public class ContentManager : IDisposable
    {
        private readonly IContentDataProvider _contentProvider;
        private readonly IMaterialsDataProvider _materialProvider;
        private readonly ISingleSelectQuestionsDataProvider _SSQProvider;
        private readonly IMultipleSelectQuestionDataProvider _MSQProvider;
        private readonly ISingleSelectImageQuestionsDataProvider _SSIQProvider;
        public ContentManager(IContentDataProvider contentProvider, IMaterialsDataProvider materialProvider,
            ISingleSelectQuestionsDataProvider SSQProvider, IMultipleSelectQuestionDataProvider MSQProvider,
            ISingleSelectImageQuestionsDataProvider SSIQProvider)
        {
            _contentProvider = contentProvider;
            _materialProvider = materialProvider;
            _SSQProvider = SSQProvider;
            _MSQProvider = MSQProvider;
            _SSIQProvider = SSIQProvider;
        }

        public void CreateContent(Content contentModel)
        {
            _contentProvider.CreateContent(contentModel);
        }

        public List<object> GetContentBySectionId(Guid id)
        {
            var contents = _contentProvider.GetContentBySectionId(id);

            var result = new List<object>();

            foreach (var el in contents)
            {
                switch (el.Type)
                {
                    case "material":
                        {
                            var materialData = _materialProvider.GetMaterialByContentId(el.Id);
                            var materialList = new List<object>();

                            var materialModel = new MaterialModel
                            {
                                Id = materialData.Id,
                                Text = materialData.Text
                            };
                            materialList.Add(materialModel);

                            var contentModel = new ContentModel
                            {
                                Id = el.Id,
                                Content = materialList,
                                Type = el.Type,
                                Title = el.Type
                            };
                            result.Add(contentModel);
                        }
                        break;
                    case "single":
                        {
                            var questionValue = _SSQProvider.GetSingleSelectQuestionByContentId(el.Id);
                            var questionValueList = new List<object>();

                            foreach (var value in questionValue)
                            {
                                var partialModel = new SingleSelectQuestionPartialModel
                                {
                                    Id = value.Id,
                                    IsAnswer = value.IsAnswer,
                                    Text = value.Text
                                };
                                questionValueList.Add(partialModel);
                            }

                            var questionModel = new ContentModel
                            {
                                Id = el.Id,
                                Type = el.Type,
                                Title = el.Title,
                                Content = questionValueList
                            };
                            result.Add(questionModel);
                        }
                        break;
                    case "multiple":
                        {
                            var questionValue = _MSQProvider.GetMultipleSelectQuestionByContentId(el.Id);
                            var questionValueList = new List<object>();

                            foreach (var value in questionValue)
                            {
                                var partialModel = new MultipleSelectQuestionPartialModel
                                {
                                    Id = value.Id,
                                    IsAnswer = value.IsAnswer,
                                    Text = value.Text
                                };
                                questionValueList.Add(partialModel);
                            }

                            var questionModel = new ContentModel
                            {
                                Id = el.Id,
                                Type = el.Type,
                                Title = el.Title,
                                Content = questionValueList
                            };
                            result.Add(questionModel);
                        }
                        break;
                    case "single_image":
                        {
                            var questionValue = _SSIQProvider.GetSingleSelectImageQuestionsByContentId(el.Id);
                            var questionValueList = new List<object>();

                            foreach (var value in questionValue)
                            {
                                var partialModel = new SingleSelectImageQuestionPartialModel
                                {
                                    Id = value.Id,
                                    IsAnswer = value.IsAnswer,
                                    Text = value.Text,
                                    Photo = value.Photo
                                };
                                questionValueList.Add(partialModel);
                            }

                            var questionModel = new ContentModel
                            {
                                Id = el.Id,
                                Type = el.Type,
                                Title = el.Title,
                                Content = questionValueList
                            };
                            result.Add(questionModel);
                        }
                        break;
                    default: break;
                }
            }

            return result;
        }

        public object GetContentById(Guid id)
        {
            var content = _contentProvider.GetContentById(id);

            switch (content.Type)
            {
                case "material":
                    {
                        var materialData = _materialProvider.GetMaterialByContentId(content.Id);
                        var materialList = new List<object>();

                        var materialModel = new MaterialModel
                        {
                            Id = materialData.Id,
                            Text = materialData.Text
                        };
                        materialList.Add(materialModel);

                        var contentModel = new ContentModel
                        {
                            Id = content.Id,
                            Content = materialList,
                            Type = content.Type,
                            Title = content.Type
                        };
                        return contentModel;
                    }
                case "single":
                    {
                        var questionValue = _SSQProvider.GetSingleSelectQuestionByContentId(content.Id);
                        var questionValueList = new List<object>();

                        foreach (var value in questionValue)
                        {
                            var partialModel = new SingleSelectQuestionPartialModel
                            {
                                Id = value.Id,
                                IsAnswer = value.IsAnswer,
                                Text = value.Text
                            };
                            questionValueList.Add(partialModel);
                        }

                        var questionModel = new ContentModel
                        {
                            Id = content.Id,
                            Type = content.Type,
                            Title = content.Title,
                            Content = questionValueList
                        };
                        return questionModel;
                    }
                case "multiple":
                    {
                        var questionValue = _MSQProvider.GetMultipleSelectQuestionByContentId(content.Id);
                        var questionValueList = new List<object>();

                        foreach (var value in questionValue)
                        {
                            var partialModel = new MultipleSelectQuestionPartialModel
                            {
                                Id = value.Id,
                                IsAnswer = value.IsAnswer,
                                Text = value.Text
                            };
                            questionValueList.Add(partialModel);
                        }

                        var questionModel = new ContentModel
                        {
                            Id = content.Id,
                            Type = content.Type,
                            Title = content.Title,
                            Content = questionValueList
                        };
                        return questionModel;
                    }
                case "single_image":
                    {
                        var questionValue = _SSIQProvider.GetSingleSelectImageQuestionsByContentId(content.Id);
                        var questionValueList = new List<object>();

                        foreach (var value in questionValue)
                        {
                            var partialModel = new SingleSelectImageQuestionPartialModel
                            {
                                Id = value.Id,
                                IsAnswer = value.IsAnswer,
                                Text = value.Text,
                                Photo = value.Photo
                            };
                            questionValueList.Add(partialModel);
                        }

                        var questionModel = new ContentModel
                        {
                            Id = content.Id,
                            Type = content.Type,
                            Title = content.Title,
                            Content = questionValueList
                        };
                        return questionModel;
                    }
                default: break;
            }

            return null;
        }

        public void EditContent(Content contentModel)
        {
            _contentProvider.EditContent(contentModel);
        }

        public void DeleteContent(Guid id)
        {
            _contentProvider.DeleteContent(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}