namespace Further_Net8_Model.Models.RootTkey
{
    /// <summary>
    /// 软删除 过滤器
    /// </summary>
    public interface IDeleteFilter
    {
        public bool IsDeleted { get; set; }
    }
}