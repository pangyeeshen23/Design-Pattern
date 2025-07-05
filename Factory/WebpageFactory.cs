using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Factory
{
    class Page
    {
        private Page()
        {
            //
        }

        private async Task<Page> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }

        public static Task<Page> CreateAsync()
        {
            Page page = new Page();
            return page.InitAsync();
        }
    }
}
