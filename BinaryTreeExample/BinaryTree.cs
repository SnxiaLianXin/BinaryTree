namespace System.Collections.Generic
{
    /// <summary>
    /// 二叉树数据类。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryTree<T> : IEnumerable<BinaryTreeNode<T>>
    {
        BinaryTreeNode<T> _Current;
        BinaryTreeNode<T> _Root;

        /// <summary>
        /// 获取当前二叉树的根节点。
        /// </summary>
        public BinaryTreeNode<T> Root => _Root;
        /// <summary>
        /// 获取二叉树当前节点的右子节点。
        /// </summary>
        public BinaryTreeNode<T> RightNode => _Current.RightNode;
        /// <summary>
        /// 获取二叉树当前节点的左子节点。
        /// </summary>
        public BinaryTreeNode<T> LeftNode => _Current.LeftNode;
        /// <summary>
        /// 获取或设置二叉树当前节点的数据。
        /// </summary>
        public T Data
        {
            get => _Current.Data;
            set => _Current.Data = value;
        }
        /// <summary>
        /// 获取或设置二叉树当前节点的子数据。
        /// </summary>
        public T SubData
        {
            get => _Current.SubData;
            set => _Current.SubData = value;
        }
        /// <summary>
        /// 获取当前节点是否为根节点。
        /// </summary>
        public bool IsRoot => _Current.IsRoot;

        /// <summary>
        /// 使用默认值初始化 <see cref="BinaryTree{T}"/> 的实例。
        /// </summary>
        public BinaryTree()
        {
            _Root = null;
            _Current = null;
        }
        /// <summary>
        /// 使用指定的数据和默认的子数据初始化 <see cref="BinaryTree{T}"/> 的实例。
        /// </summary>
        /// <param name="data">指定初始化的数据。</param>
        public BinaryTree(T data)
        {
            _Root = new BinaryTreeNode<T>(data, default);
            _Current = _Root;
        }
        /// <summary>
        /// 使用指定的数据和子数据初始化 <see cref="BinaryTree{T}"/> 的实例。
        /// </summary>
        /// <param name="data">指定初始化的数据。</param>
        /// <param name="subData">指定初始化的子数据。</param>
        public BinaryTree(T data, T subData)
        {
            _Root = new BinaryTreeNode<T>(data, subData);
            _Current = _Root;
        }

        public void Insert()
        {
            if( _Root is null )
            {
                _Current =
                _Root = new BinaryTreeNode<T>();
            }
            else
            {
                _Current = new BinaryTreeNode<T>();
            }
        }
        /// <summary>
        /// 将一个 <see cref="BinaryTreeNode{T}"/> 插入到树中。
        /// </summary>
        /// <param name="value">要插入的节点数据。</param>
        public void Insert(BinaryTreeNode<T> value)
        {
            if( _Root is null )
            {
                _Current =
                _Root = InsertInternal(_Current, value.Data, value.SubData);
            }
            else
            {
                _Current = InsertInternal(_Current, value.Data, value.SubData);
            }
        }
        /// <summary>
        /// 将一个指定的数据插入到树中。
        /// </summary>
        /// <param name="data">要插入的数据。</param>
        public void Insert(T data)
        {
            if( _Root is null )
            {
                _Current =
                _Root = InsertInternal(_Current, data, default);
            }
            else
            {
                _Current = InsertInternal(_Current, data, default);
            }
        }
        /// <summary>
        /// 将一个指定的数据和子数据插入到树中。
        /// </summary>
        /// <param name="data">要插入的数据。</param>
        /// <param name="subData">要插入的子数据。</param>
        public void Insert(T data, T subData)
        {
            if( _Root is null )
            {
                _Root =
                _Current = InsertInternal(_Current, data, subData);
            }
            else
            {
                _Current = InsertInternal(_Current, data, subData);
            }
        }
        /// <summary>
        /// 将二叉树当前节点设置为右子节点。
        /// </summary>
        /// <returns>如果成功将当前节点设置为右子节点返回 <see langword="true"/>；否则为 <see langword="false"/>。</returns>
        public bool NextRight()
        {
            if( _Current is null || _Current.RightNode is null ) return false;
            _Current = _Current.RightNode;
            return true;
        }
        /// <summary>
        /// 将二叉树当前节点设置为左子节点。
        /// </summary>
        /// <returns>如果成功将当前节点设置为左子节点返回 <see langword="true"/>；否则为 <see langword="false"/>。</returns>
        public bool NextLeft()
        {
            if( _Current is null || _Current.LeftNode is null ) return false;
            _Current = _Current.LeftNode;
            return true;
        }
        /// <summary>
        /// 将二叉树当前节点设置为节点的父节点。
        /// </summary>
        /// <returns>如果成功将当前节点设置为父节点返回 <see langword="true"/>；否则为 <see langword="false"/>。</returns>
        public bool Previous()
        {
            if( _Current is null || _Current.Parent is null ) return false;
            _Current = _Current.Parent;
            return true;
        }
        /// <summary>
        /// 获取当前二叉树节点位于树中的层数。
        /// </summary>
        /// <returns>一个表示当前节点位于树中的层数从0开始的值；如果当前实例为 <see langword="null"/> ，则为 -1。</returns>
        public int Layer()
        {
            if( _Current is null ) return -1;
            int result = 0;
            BinaryTree<T> _Temp = this;
            while( _Temp.Previous() )
            {
                result++;
            }
            return result;
        }
        /// <summary>
        /// 将 <see cref="BinaryTree{T}"/> 转换为 <see cref="List{T}"/> 列表。
        /// </summary>
        /// <returns>一个 <see cref="List{T}"/> 列表，其中包含 <see cref="BinaryTree{T}"/> 的节点。</returns>
        public List<BinaryTreeNode<T>> ToList()
        {
            List<BinaryTreeNode<T>> result = new List<BinaryTreeNode<T>>();
            Traversal(_Root, result);
            return result;
        }

        private BinaryTreeNode<T> InsertInternal(BinaryTreeNode<T> source, T data, T subData)
        {
            if( source is null ) return new BinaryTreeNode<T>(data, subData);
            if( Comparer.Default.Compare(source.Data, data) > 0 )
            {
                if( source.LeftNode is null ) return source.LeftNode = new BinaryTreeNode<T>(data, subData, source);
                else return InsertInternal(source.LeftNode, data, subData);
            }
            else
            {
                if( source.RightNode is null ) return source.RightNode = new BinaryTreeNode<T>(data, subData, source);
                else return InsertInternal(source.RightNode, data, subData);
            }
        }
        private void Traversal(BinaryTreeNode<T> value, List<BinaryTreeNode<T>> result)
        {
            if( value != null )
            {
                result.Add(value);
                Traversal(value.LeftNode, result);
                Traversal(value.RightNode, result);
            }
        }

        IEnumerator<BinaryTreeNode<T>> IEnumerable<BinaryTreeNode<T>>.GetEnumerator()
        {
            foreach( BinaryTreeNode<T> result in ToList() )
            {
                yield return result;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach( BinaryTreeNode<T> result in ToList() )
            {
                yield return result;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is BinaryTree<T> tree &&
                   EqualityComparer<BinaryTreeNode<T>>.Default.Equals(_Current, tree._Current) &&
                   EqualityComparer<BinaryTreeNode<T>>.Default.Equals(_Root, tree._Root) &&
                   EqualityComparer<BinaryTreeNode<T>>.Default.Equals(Root, tree.Root) &&
                   EqualityComparer<BinaryTreeNode<T>>.Default.Equals(RightNode, tree.RightNode) &&
                   EqualityComparer<BinaryTreeNode<T>>.Default.Equals(LeftNode, tree.LeftNode) &&
                   IsRoot == tree.IsRoot;
        }
        public override int GetHashCode()
        {
            int hashCode = -686875623;
            hashCode = hashCode * -1521134295 + EqualityComparer<BinaryTreeNode<T>>.Default.GetHashCode(_Current);
            hashCode = hashCode * -1521134295 + EqualityComparer<BinaryTreeNode<T>>.Default.GetHashCode(_Root);
            return hashCode;
        }
        public override string ToString()
        {
            return _Current.ToString();
        }
    }

    /// <summary>
    /// 二叉树数据节点类。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryTreeNode<T> : IDisposable
    {
        BinaryTreeNode<T> LeftChild = null;
        BinaryTreeNode<T> RightChild = null;
        readonly BinaryTreeNode<T> _Parent = null;

        /// <summary>
        /// 获取或设置当前节点的数据。
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 获取或设置当前节点的子数据。
        /// </summary>
        public T SubData { get; set; }
        /// <summary>
        /// 获取当前节点是否为根节点。
        /// </summary>
        public bool IsRoot => _Parent is null;
        /// <summary>
        /// 获取当前节点的父节点。
        /// </summary>
        public BinaryTreeNode<T> Parent => _Parent;
        /// <summary>
        /// 获取或设置当前节点的左子节点。
        /// </summary>
        public BinaryTreeNode<T> LeftNode
        {
            get => LeftChild;
            set => LeftChild = value;
        }
        /// <summary>
        /// 获取或设置当前节点的右子节点。
        /// </summary>
        public BinaryTreeNode<T> RightNode
        {
            get => RightChild;
            set => RightChild = value;
        }

        /// <summary>
        /// 用默认的值初始化 <see cref="BinaryTreeNode{T}"/> 的实例。
        /// </summary>
        public BinaryTreeNode()
        {
            Data = default;
            SubData = default;
        }
        /// <summary>
        /// 用指定的数据初始化 <see cref="BinaryTreeNode{T}"/> 的实例。
        /// </summary>
        /// <param name="value">要初始化的数据。</param>
        public BinaryTreeNode(T value)
        {
            Data = value;
            SubData = default;
        }
        /// <summary>
        /// 用指定的数据和子数据初始化 <see cref="BinaryTreeNode{T}"/> 的实例。
        /// </summary>
        /// <param name="value">要初始化的数据。</param>
        /// <param name="subData">要初始化的子数据。</param>
        public BinaryTreeNode(T value, T subData)
        {
            Data = value;
            SubData = subData;
        }
        /// <summary>
        /// 用指定的数据、子数据和父节点初始化 <see cref="BinaryTreeNode{T}"/> 的实例。
        /// </summary>
        /// <param name="value">要初始化的数据。</param>
        /// <param name="subData">要初始化的子数据。</param>
        /// <param name="parent">指定的父节点。</param>
        public BinaryTreeNode(T value, T subData, BinaryTreeNode<T> parent)
        {
            Data = value;
            SubData = subData;
            _Parent = parent;
        }
        ~BinaryTreeNode()
        {
            Dispose(false);
        }

        public override bool Equals(object obj)
        {
            return obj is BinaryTreeNode<T> node &&
                   EqualityComparer<T>.Default.Equals(Data, node.Data) &&
                   EqualityComparer<T>.Default.Equals(SubData, node.SubData) &&
                   EqualityComparer<BinaryTreeNode<T>>.Default.Equals(LeftNode, node.LeftNode) &&
                   EqualityComparer<BinaryTreeNode<T>>.Default.Equals(RightNode, node.RightNode) &&
                   IsRoot == node.IsRoot;
        }
        public override int GetHashCode()
        {
            int hashCode = -1984107771;
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(Data);
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(SubData);
            hashCode = hashCode * -1521134295 + IsRoot.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<BinaryTreeNode<T>>.Default.GetHashCode(LeftNode);
            hashCode = hashCode * -1521134295 + EqualityComparer<BinaryTreeNode<T>>.Default.GetHashCode(RightNode);
            return hashCode;
        }
        public override string ToString()
        {
            return Data.ToString();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposeManageObject)
        {
            if( disposeManageObject )
            {
                if( Data != null )
                {
                    if( typeof(IDisposable).IsAssignableFrom(Data.GetType()) )
                    {
                        ((IDisposable) Data).Dispose();
                    }
                    Data = default;
                }
                if( SubData != null )
                {
                    if( typeof(IDisposable).IsAssignableFrom(SubData.GetType()) )
                    {
                        ((IDisposable) SubData).Dispose();
                    }
                    SubData = default;
                }
                if( LeftNode != null )
                {
                    LeftNode.Dispose();
                    LeftNode = null;
                }
                if( RightNode != null )
                {
                    RightNode.Dispose();
                    RightNode = null;
                }
            }
        }
    }
}
