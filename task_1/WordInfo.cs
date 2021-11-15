using System.Collections.Generic;

namespace task_1
{
    class WordInfo
    {
        private List<int> line = new List<int>();
        private List<int> pos = new List<int>();

        public List<int> getLine()
        {
            return this.line;
        }

        public void setLine(int n)
        {
            line.Add(n);
        }

        public List<int> getPos()
        {
            return this.pos;
        }

        public void setPos(int n)
        {
            pos.Add(n);
        }
    }
}
