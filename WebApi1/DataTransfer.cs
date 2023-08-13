using WebApi1.Dtos;

namespace WebApi1
{
    public class DataTransfer
    {
        static WebApi1Dto _data;

        public static WebApi1Dto GetData()
        {
            if (_data == null)
            {
                return new WebApi1Dto
                {
                    Id = 1,
                    Name="WebApi1",
                    Port=5041
                };
            }

            return _data;
        }

        public static void PostData(WebApi1RequestDto webApi1RequestDto)
        {
            _data = new WebApi1Dto
            {
                Id = webApi1RequestDto.Id,
                Name = webApi1RequestDto.Name,
                Port=5041
            };
        }
    }
}
