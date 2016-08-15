using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Question : Entity
    {
        public virtual Section Section { get; protected internal set; }
        public string Type { get; protected internal set; }

        public virtual ICollection<Answer> AnswersCollection { get; protected internal set; }

        public Question()
            : base()
        {
            AnswersCollection = new List<Answer>();
        }

        public Question(string title, string userName, Section section, string type) :
            this()
        {
            ThrowIfTileInvalid(title);
            ThrowIfTypeInvalid(type);
            ThrowIfUserNameInvalid(userName);

            Title = title;
            Type = type;
            Section = section;
            CreatedBy = ModifiedBy = userName;
        }

        #region Answers state modify

        public void SetCorrectAnswer(string answerId, string userName)
        {
            var parsedAnswerId = ThrowIfAnswerIdIsParseFaild(answerId);
            ThrowIfUserNameInvalid(userName);

            var currentTrueAnswer = AnswersCollection.First(m => m.IsCorrect == true);
            var newTrueAnswer = AnswersCollection.First(m => m.Id == parsedAnswerId);

            currentTrueAnswer.UpdateState(false);
            newTrueAnswer.UpdateState(true);

            MarkAsModified(userName);
        }

        #endregion

        #region Validation

        private void ThrowIfTypeInvalid(string type)
        {
            if (type != "material" && type != "single" && type != "multiple" && type != "single_image")
                throw new ArgumentException("Type is not valid.");
        }

        private Guid ThrowIfAnswerIdIsParseFaild(string answerId)
        {
            var correctAnswerId = Guid.Empty;
            var tryParseId = Guid.TryParse(answerId, out correctAnswerId);

            if (!tryParseId)
                throw new ArgumentException("Invalid data");

            return correctAnswerId;
        }
        #endregion
    }
}
