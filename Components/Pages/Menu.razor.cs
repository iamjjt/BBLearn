using BlazorLearWebApp.Entity;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorLearWebApp.Components.Pages
{
    public partial class Menu
    {
        public List<TreeViewItem<int>> MenuTreeData { get; set; } = [];

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await RefreshData();
        }
        
        private async Task RefreshData()
        {
            MenuTreeData.Clear();
            var menus = await MenuEntity.Select.ToListAsync();
            MenuTreeData.Add(new TreeViewItem<int>(0) { Text = "顶级菜单", Value = 0});
            MenuTreeData.AddRange(CascadingMenu(menus, 0));
        }
        private List<TreeViewItem<int>> CascadingMenu(List<MenuEntity> menus, int i)
        {
            return menus.Where(x => x.ParentId == i)
                .Select(x => new TreeViewItem<int>(x.Id)
                {
                    Text = x.MenuName, 
                    Value =  x.Id,
                    Items = CascadingMenu(menus, x.Id)
                }).ToList();
        }
        
        private async Task<bool> OnSaveAsync(MenuEntity arg1, ItemChangedType arg2)
        {
            await arg1.SaveAsync();
            await RefreshData();
            return true;
        }

        [Inject]
        private IDataService<MenuEntity> _dataService { get; set; }
        private async Task<bool> OnDeleteAsync(IEnumerable<MenuEntity> arg)
        {
            if (!await _dataService.DeleteAsync(arg)) return false;
            await RefreshData();
            return true;

        }
        
        private Task<IEnumerable<TableTreeNode<MenuEntity>>> TreeNodeConverter(IEnumerable<MenuEntity> items)
        {
            // 构造树状数据结构
            var ret = BuildTreeNodes(items, 0);
            return Task.FromResult(ret);

            IEnumerable<TableTreeNode<MenuEntity>> BuildTreeNodes(IEnumerable<MenuEntity> items, int parentId)
            {
                var ret = new List<TableTreeNode<MenuEntity>>();
                ret.AddRange(items.Where(i => i.ParentId == parentId).Select((foo, index) => new TableTreeNode<MenuEntity>(foo)
                {
                    // 此处为示例，假设偶行数据都有子数据
                    HasChildren =  items.Any(i => i.ParentId == foo.Id),
                    // 如果子项集合有值 则默认展开此节点
                    IsExpand = items.Any(i => i.ParentId == foo.Id),
                    // 获得子项集合
                    Items = BuildTreeNodes(items, foo.Id)
                }));
                return ret;
            }
        }
    }
}