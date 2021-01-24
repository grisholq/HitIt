namespace HitIt.Ecs
{
    public class LogRotationIterator : IIterator<LogRotationNode>
    {
        private LogRotationNode[] nodes;
        private int index;
        private int length;

        public LogRotationIterator(LogRotationNode[] nodes)
        {
            this.nodes = nodes;
            length = nodes.Length;
            index = 0;
        }

        public bool HasNext()
        {
            return index < length;
        }

        public LogRotationNode Next()
        {            
            return nodes[index++];
        }

        public void ToBegin()
        {
            index = 0; 
        }
    }
}