using System.ComponentModel;
using FreeSql.DataAnnotations;

namespace BlazorLearWebApp.Entity;
[Description("角色菜单关系表")]
public class RoleMenuEntity
{
    [Column(IsPrimary = true)]
    public int RoleId { get; set; }
    [Column(IsPrimary = true)]
    public int MenuId { get; set; }
    
    [Navigate(ManyToMany = typeof(RoleMenuEntity))]
    public List<MenuEntity>? Menus { get; set; }
    
}