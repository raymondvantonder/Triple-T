using TripleT.User.Application.SummaryProduct.Commands.CreateSummaryProduct;
using TripleT.User.Domain.Domain;

namespace TripleT.User.Application.SummaryProduct
{
    public static class SummaryProductMappingExtensions
    {
        public static SummaryProductDto ToDto(this SummaryProductEntity entity)
        {
            if (entity == null) return null;

            return new SummaryProductDto
            {
                Description = entity.Description,
                Id = entity.Id,
                Price = entity.Price,
                Title = entity.Title,
                CreatedTime = entity.CreatedTime,
                FileLocation = entity.FileLocation,
                GradeId = entity.GradeId,
                IconUri = entity.IconUri,
                SubjectId = entity.SubjectId,
                UpdatedTime = entity.UpdatedTime,
                FileSizeInMb = entity.FileSizeInMb
            };
        }
    }
}