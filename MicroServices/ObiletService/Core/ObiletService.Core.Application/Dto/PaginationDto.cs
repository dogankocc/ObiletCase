
namespace ObiletService.Core.Application.Dto
{
    public class PaginationDto
    {
        public int Page { get; set; } = 0;
        public int RowsPerPage { get; set; } = 10;

        public int Skip { get { return (Page) * RowsPerPage; } set { Skip = value; } }
        public int Take { get { return RowsPerPage; } set { Take = value; } }
    }
}
