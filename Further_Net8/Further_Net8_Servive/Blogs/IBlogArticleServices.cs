using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Further_Net8_Model.Models;
using Further_Net8_Model.ViewModels;
using Further_Net8_Servive.Base;

namespace Further_Net8_Servive.Blogs
{
    public interface IBlogArticleServices : IBaseServices<BlogArticle>
    {
        Task<List<BlogArticle>> GetBlogs();
        Task<BlogViewModels> GetBlogDetails(long id);

    }
}
