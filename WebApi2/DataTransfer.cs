using WebApi2.Dtos;

namespace WebApi2
{
    public class DataTransfer
    {
        static WebApi2Dto _data;

        public static WebApi2Dto GetData()
        {
            if(_data == null)
            {
                return new WebApi2Dto
                {
                    Id = 1,
                    Name="WebApi1",
                    Port=5042
                };
            }

            return _data;
        }

        public static void PostData(WebApi2RequestDto webApi2RequestDto)
        {
            _data = new WebApi2Dto
            {
                Id = webApi2RequestDto.Id,
                Name = webApi2RequestDto.Name,
                Port=5042
            };
        }
    }
}
