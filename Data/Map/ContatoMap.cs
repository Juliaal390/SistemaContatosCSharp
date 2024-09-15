using CrudMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrudMVC.Data.Map
{
    public class ContatoMap : IEntityTypeConfiguration<ContatoModel>
    {
        public void Configure(EntityTypeBuilder<ContatoModel> builder)
        {
            builder.HasKey(x => x.Id); //indica que a chave primária é identificada pelo ID
            builder.HasOne(x => x.Usuario);
        }
    }
}
