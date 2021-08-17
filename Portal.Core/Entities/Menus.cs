using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Menus : BaseEntity
    {
        public string IDName { get; set; }
        public string Controller { get; set; }
        public int Ordem { get; set; }
        public Int64 ParentID { get; set; }
        public int Sequence { get; set; }
        public string Descricao { get; set; }
        public string ActionName { get; set; }
        public int interno { get; set; }
        public string Grupo { get; set; }
    }
}
