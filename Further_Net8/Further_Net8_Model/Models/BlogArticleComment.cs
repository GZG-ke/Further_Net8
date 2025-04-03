using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Further_Net8_Model.Models.RootTkey;
using SqlSugar;

namespace Further_Net8_Model.Models
{
    /// <summary>
    /// 博客文章 评论
    /// </summary>
    public class BlogArticleComment : RootEntityTkey<long>
    {
        public long bID { get; set; }

        public string Comment { get; set; }


        public string UserId { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(UserId))]
        public SysUserInfo User { get; set; }
    }
}
