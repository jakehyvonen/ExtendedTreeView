using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTreeView
{
    public class ItemProvider
    {
        //get DirectoryItems until a specified stopDepth: /fp/1/2/.../stopDepth
        private int depthCounter = 0;
        public List<Item> GetItems(string fp, int stopDepth = 4)
        {
            if (depthCounter <= stopDepth)
            {
                depthCounter++;
            }
            var items = new List<Item>();
            var dirInfo = new DirectoryInfo(fp);

            foreach (var dir in dirInfo.GetDirectories())
            {
                var item = new DirectoryItem
                {
                    Name = dir.Name,
                    Path = dir.FullName
                };
                items.Add(item);
            }
            if (depthCounter < stopDepth)
            {
                depthCounter++;
                foreach (DirectoryItem item in items)
                {
                    item.Items = GetItems(item.Path);
                }
                if (depthCounter == stopDepth)
                {
                    depthCounter -= 2;
                }
            }
            if (depthCounter >= stopDepth)
            {
                depthCounter--;
            }
            return items;
        }
    }
}
