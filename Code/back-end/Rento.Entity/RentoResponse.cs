using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{

    public class RentoResponse
    {
        private readonly static Dictionary<ErrorCode, ErrorMessageLan> _ErrorCodeText = Entity.ErrorMessageLan.InitiatMessages();

        public Rento.Entity.ErrorCode ErrorCode { get; set; }
        public Rento.Entity.Language Language { get; set; }
        public int RowsCount { get; set; }
        public string DeveloperMessage { get; set; }
        public RentoResponse(RentoRequest req)
        {
            if (req == null)
                Language = (int)Language.Arabic;
            else
                Language = (Language)req.Language;
        }
        public bool Success
        {
            get
            {
                return this.ErrorCode == ErrorCode.Success;
            }
        }
        public string Message
        {
            get
            {
                if (!_ErrorCodeText.ContainsKey(this.ErrorCode))
                    return ErrorCode.ToString();
                var value = _ErrorCodeText[this.ErrorCode];
                return this.Language == Language.Arabic ? value.ArabicText : value.EnglishText;
            }
        }

    }

    public class RentoResponse<T> : RentoResponse
    {
        public RentoResponse(RentoRequest req) : base(req)
        {

        }
        public T Data { get; set; }
    }
    public class RentoCountResponse<T> 
    {
        public int RowsCount { get; set; }
        public T Data { get; set; }
    }
}
