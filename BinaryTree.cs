using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.BinaryTrees
{

    public class BinaryTree 
    {
        public static int[] Create(params int[] vs)
        {
            int[] a = new int[vs.Length];
            Array.Sort(vs);
            a = vs;
            return a;
        }
    }


    public class BinaryTree<T> : IEnumerable where T : IComparable<T>
    {
        private bool bRootExists;

        public T Value;                // Значение в текущей вершине

        public BinaryTree<T> Parent;   // Ссылка на родителя
        public BinaryTree<T> Left;     // Ссылка на левый элемент
        public BinaryTree<T> Right;    // Ссылка на правый элемент

        //private List<T> listForPrint = new List<T>();

        //Create a public GetEnumerator method, the basic ingredient of an IEnumerable interface.
        public IEnumerator GetEnumerator()
        {
            return null;
        }


        public BinaryTree(T val, BinaryTree<T> parent)
        {
            this.Value = val;
            this.Parent = parent;
        }

        public BinaryTree()
        {
            bRootExists = false; 
        }

        // Добавление нового элемента в дерево        
        public void Add(T val)
        {
            // Если корневой элемент пустой
            if ((this.Parent == null) & (bRootExists == false))
            {
                bRootExists = true;
                this.Value = val;
            }

            // Если новое число меньше текущего
            else
            if (val.CompareTo(this.Value) <= 0)
            {
                if (this.Left == null)
                {
                    this.Left = new BinaryTree<T>(val, this);
                }
                else if (this.Left != null)
                    this.Left.Add(val);
            }
            
            // Если новое число больше или равно текущему 
            else
            {
                if (this.Right == null)
                {
                    this.Right = new BinaryTree<T>(val, this);
                }
                else if (this.Right != null)
                    this.Right.Add(val);
            }
        }

        // Внутренняя рекурсивная процедура поиска
        private BinaryTree<T> _search(BinaryTree<T> tree, T val)
        {
            if (tree == null) return null;
            switch (val.CompareTo(tree.Value))
            {
                case 1: return _search(tree.Right, val);
                case -1: return _search(tree.Left, val);
                case 0: return tree;
                default: return null;
            }
        }
        
        // Поиск элемена в дереве
        public BinaryTree<T> Search(T val)
        {
            return _search(this, val);
        }
        
        // Удаление элемента из дерева
        public bool Remove(T val)
        {
            //Проверяем, существует ли данный узел
            BinaryTree<T> tree = Search(val);
            if (tree == null)
            {
                //Если узла не существует, вернем false
                return false;
            }
            BinaryTree<T> curTree;

            //Если удаляем корень
            if (tree == this)
            {
                if (tree.Right != null)
                {
                    curTree = tree.Right;
                }
                else curTree = tree.Left;

                while (curTree.Left != null)
                {
                    curTree = curTree.Left;
                }
                T temp = curTree.Value;
                this.Remove(temp);
                tree.Value = temp;

                return true;
            }

            //Удаление листьев
            if (tree.Left == null && tree.Right == null && tree.Parent != null)
            {
                if (tree == tree.Parent.Left)
                    tree.Parent.Left = null;
                else
                {
                    tree.Parent.Right = null;
                }
                return true;
            }

            //Удаление узла, имеющего левое поддерево, но не имеющее правого поддерева
            if (tree.Left != null && tree.Right == null)
            {
                //Меняем родителя
                tree.Left.Parent = tree.Parent;
                if (tree == tree.Parent.Left)
                {
                    tree.Parent.Left = tree.Left;
                }
                else if (tree == tree.Parent.Right)
                {
                    tree.Parent.Right = tree.Left;
                }
                return true;
            }

            //Удаление узла, имеющего правое поддерево, но не имеющее левого поддерева
            if (tree.Left == null && tree.Right != null)
            {
                //Меняем родителя
                tree.Right.Parent = tree.Parent;
                if (tree == tree.Parent.Left)
                {
                    tree.Parent.Left = tree.Right;
                }
                else if (tree == tree.Parent.Right)
                {
                    tree.Parent.Right = tree.Right;
                }
                return true;
            }

            //Удаляем узел, имеющий поддеревья с обеих сторон
            if (tree.Right != null && tree.Left != null)
            {
                curTree = tree.Right;

                while (curTree.Left != null)
                {
                    curTree = curTree.Left;
                }

                //Если самый левый элемент является первым потомком
                if (curTree.Parent == tree)
                {
                    curTree.Left = tree.Left;
                    tree.Left.Parent = curTree;
                    curTree.Parent = tree.Parent;
                    if (tree == tree.Parent.Left)
                    {
                        tree.Parent.Left = curTree;
                    }
                    else if (tree == tree.Parent.Right)
                    {
                        tree.Parent.Right = curTree;
                    }
                    return true;
                }
                //Если самый левый элемент НЕ является первым потомком
                else
                {
                    if (curTree.Right != null)
                    {
                        curTree.Right.Parent = curTree.Parent;
                    }
                    curTree.Parent.Left = curTree.Right;
                    curTree.Right = tree.Right;
                    curTree.Left = tree.Left;
                    tree.Left.Parent = curTree;
                    tree.Right.Parent = curTree;
                    curTree.Parent = tree.Parent;
                    if (tree == tree.Parent.Left)
                    {
                        tree.Parent.Left = curTree;
                    }
                    else if (tree == tree.Parent.Right)
                    {
                        tree.Parent.Right = curTree;
                    }

                    return true;
                }
            }
            return false;
        }

        /*private void _print(BinaryTree<T> node)
        {
            if (node == null) return;
            _print(node.Left);
            listForPrint.Add(node.Value);
            Console.Write(node + " ");
            if (node.Right != null)
                _print(node.Right);
        }
        public void print()
        {
            listForPrint.Clear();
            _print(this);
            Console.WriteLine();
        }

        public override string ToString()
        {
            return Value.ToString();
        }*/


    }
}
