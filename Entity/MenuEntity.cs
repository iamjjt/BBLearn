using System.ComponentModel;
using FreeSql;
using FreeSql.DataAnnotations;

namespace BlazorLearWebApp.Entity;
[Description("菜单信息表")]
public class MenuEntity: BaseEntity<MenuEntity,int>
{
    [Description("菜单名称")]
    public string? MenuName { get; set; }
    [Description("菜单地址")]
    public string? Url { get; set; }
    [Description("菜单图标")]
    public string? Icon { get; set; }

    [Description("父级菜单ID")] 
    public int ParentId { get; set; } 

    [Navigate(nameof(ParentId))]
    public MenuEntity? Parent { get; set; }
    [Navigate(nameof(ParentId))]
    public List<MenuEntity>? Children { get; set; }
    
    
    [Navigate(ManyToMany = typeof(RoleMenuEntity))]
    public List<RoleEntity>? Roles { get; set; }
}