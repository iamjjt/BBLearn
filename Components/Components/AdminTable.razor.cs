using System.Diagnostics.CodeAnalysis;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorLearWebApp.Components.Components
{
    [CascadingTypeParameter(nameof(TItem))]
    public partial class AdminTable<TItem> where TItem : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        [Parameter]
        public RenderFragment<TItem>? TableColumns { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? EditTemplate { get; set; }
        
        [Parameter] 
        public Func<TItem,ItemChangedType,Task<bool>>? OnSaveAsync { get; set; }
        
        [Parameter] 
        public Func<TItem,Task>? OnEditAsync { get; set; }
        [Parameter] 
        public Func<IEnumerable<TItem>,Task<bool>>? OnDeleteAsync { get; set; }
        
        [Parameter] public bool IsTree { get; set; }
        
        [Parameter] public Func<IEnumerable<TItem>,Task<IEnumerable<TableTreeNode<TItem>>>>? TreeNodeConverter { get; set; }
    }
}