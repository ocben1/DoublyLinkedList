using System;
using System.Text;

namespace DoublyLinkedList
{
    public class DoublyLinkedList<T>
    {

        // Here is the the nested Node<K> class 
        private class Node<K> : INode<K>
        {
            public K Value { get; set; }
            public Node<K> Next { get; set; }
            public Node<K> Previous { get; set; }

            public Node(K value, Node<K> previous, Node<K> next)
            {
                Value = value;
                Previous = previous;
                Next = next;
            }

            // This is a ToString() method for the Node<K>
            // It represents a node as a tuple {'the previous node's value'-(the node's value)-'the next node's value')}. 
            // 'XXX' is used when the current node matches the First or the Last of the DoublyLinkedList<T>
            public override string ToString()
            {
                StringBuilder s = new StringBuilder();
                s.Append("{");
                s.Append(Previous.Previous == null ? "XXX" : Previous.Value.ToString());
                s.Append("-(");
                s.Append(Value);
                s.Append(")-");
                s.Append(Next.Next == null ? "XXX" : Next.Value.ToString());
                s.Append("}");
                return s.ToString();
            }

        }

        // Here is where the description of the methods and attributes of the DoublyLinkedList<T> class starts

        // An important aspect of the DoublyLinkedList<T> is the use of two auxiliary nodes: the Head and the Tail. 
        // The both are introduced in order to significantly simplify the implementation of the class and make insertion functionality reduced just to a AddBetween(...)
        // These properties are private, thus are invisible to a user of the data structure, but are always maintained in it, even when the DoublyLinkedList<T> is formally empty. 
        // Remember about this crucial fact when you design and code other functions of the DoublyLinkedList<T> in this task.
        private Node<T> Head { get; set; }
        private Node<T> Tail { get; set; }
        public int Count { get; private set; } = 0;
        //Initializes a new instance of the DoublyLinkedList<T> class that is empty.
        public DoublyLinkedList()
        {
            Head = new Node<T>(default(T), null, null);
            Tail = new Node<T>(default(T), Head, null);
            Head.Next = Tail;
        }
        //Property.Gets the first node of the DoublyLinkedList<T>.If the DoublyLinkedList<T> is empty, the First property
        //returns null
        public INode<T> First
        {
            get
            {
                if (Count == 0) return null;
                else return Head.Next;
            }
        }

        public INode<T> Last
        {
            get
            {
                if (Count == 0) return null;
                else return Tail.Previous;
            }
        }
        /*Returns the node casted to the INode<T> that succeeds the specified node in the DoublyLinkedList<T>.If the
        node given as parameter is null, it throws the ArgumentNullException. If the parameter is not in the current
        DoublyLinkedList<T>, the method throws the InvalidOperationException.*/
        public INode<T> After(INode<T> node)
        {
            if (node == null) throw new NullReferenceException();
            Node<T> node_current = node as Node<T>;
            if (node_current.Previous == null || node_current.Next == null) throw new InvalidOperationException("The node referred as 'before' is no longer in the list");
            if (node_current.Next.Equals(Tail)) return null;
            else return node_current.Next;
        }
        /*Adds a new node containing the specified value at the end of the DoublyLinkedList<T>. Returns the new node
       casted to the INode<T> with the recorded value.*/
        public INode<T> AddLast(T value)
        {
            return AddBetween(value, Tail.Previous, Tail);
        }
        /*An important aspect of the DoublyLinkedList<T> is the use of two auxiliary nodes: the Head
        and the Tail.The both are introduced in order to significantly simplify the implementation of the class and
        make insertion functionality reduced just to a single method designated here as
        Node<T> AddBetween(T value, Node<T> previous, Node<T> next)
        In fact, the Head and the Tail are invisible to a user of the data structure and are always maintained in it,
        even when the DoublyLinkedList<T> is formally empty. When there is no element in it, the Head refers to
        the Tail, and vice versa.Note that in this case the First and the Last properties are set to null. The first
        added node therefore is to be placed in between the Head and the Tail so that the former points to the
        new node as the Next node, while the latter points to it as the Previous node.Hence, from the perspective
        of the internal structure of the DoublyLinkedList<T>, the First element is the next to the Head, and
        similarly, the Last element is previous to the Tail.
        // This is a private method that creates a new node and inserts it in between the two given nodes referred as the previous and the next.
        // Use it when you wish to insert a new value (node) into the DoublyLinkedList<T>*/
        // This is a private method that creates a new node and inserts it in between the two given nodes referred as the previous and the next.
        // Use it when you wish to insert a new value (node) into the DoublyLinkedList<T>
        private Node<T> AddBetween(T value, Node<T> previous, Node<T> next)
        {
            Node<T> node = new Node<T>(value, previous, next); //Node<E> Newest = new Node<>(E e, Node<E> predecessor, Node<E> successor)
            previous.Next = node; //predecessor.setNext(newest)
            next.Previous = node; //successor.setPrev(newest)
            Count++;
            return node;
        }
        /*Finds the first occurrence in the DoublyLinkedList<T> that contains the specified value. The method returns the
        node casted to INode<T>, if found; otherwise, null. The DoublyLinkedList<T> is searched forward starting at First
        and ending at Last.*/
        public INode<T> Find(T value)
        {
            Node<T> node = Head.Next;
            while (!node.Equals(Tail))
            {
                if (node.Value.Equals(value)) return node;
                node = node.Next;
            }
            return null;
        }
        /*Returns a string that represents the current DoublyLinkedList<T>.ToString() is the major formatting method in
        the.NET Framework.It converts an object to its string representation so that it is suitable for display.*/
        public override string ToString()
        {
            if (Count == 0) return "[]";
            StringBuilder s = new StringBuilder();
            s.Append("[");
            int k = 0;
            Node<T> node = Head.Next;
            while (!node.Equals(Tail))
            {
                s.Append(node.ToString());
                node = node.Next;
                if (k < Count - 1) s.Append(",");
                k++;
            }
            s.Append("]");
            return s.ToString();
        }

        // TODO: Your task is to implement all the remaining methods.
        // Read the instruction carefully, study the code examples from above as they should help you to write the rest of the code.

        /*Returns the node, casted to the INode<T>, which precedes the specified node in the DoublyLinkedList<T>. */
        public INode<T> Before(INode<T> node)
        {
            //If the node given as parameter is null, the method throws the ArgumentNullException.
            if (node == null) throw new NullReferenceException();
            Node<T> node_current = node as Node<T>;
            //If the parameter is not in the current DoublyLinkedList<T>, the method throws the InvalidOperationException.
            if (node_current.Previous == null || node_current.Next == null || Find(node.Value) == null) throw new InvalidOperationException("The node referred as 'before' is no longer in the list");
            //if the node's previous element is adjacent to the head, return null.
            if (node_current.Previous.Equals(Head)) return null;
            //otherwise return the specified node 'before'
            else return node_current.Previous;
        }

        /*Adds a new node containing the specified value at the start of the DoublyLinkedList<T>. Returns the new node
        casted to the INode<T> containing the value.*/
        public INode<T> AddFirst(T value)
        {
            return AddBetween(value, Head, Head.Next);
        }

        /*Adds a new node before the specified node of the DoublyLinkedList<T> and records the given value as its payload.
         * It returns the newly created node casted to the INode<T>. If the node specified as an argument is null, the
         * method throws the ArgumentNullException. If the node specified as argument does not exist in the
         * DoublyLinkedList<T>, the method throws the InvalidOperationException.*/
        public INode<T> AddBefore(INode<T> before, T value)
        {
            if (before == null) throw new NullReferenceException();
            Node<T> node_current = before as Node<T>;
            //if node's neighbours are null or specified node is not in the list, throw an exception
            if (node_current.Previous == null || node_current.Next == null || Find(before.Value) == null)
            {
                throw new InvalidOperationException("The node referred as 'before' is no longer in the list");
            }
            //inserts value T before the specified node
            return AddBetween(value, node_current.Previous, node_current);
        }
        /*Adds a new node after the specified node of the DoublyLinkedList<T> and records the given value as its payload.
        It returns the newly created node casted to the INode<T>. If the node specified as argument is null, the method
        throws the ArgumentNullException. If the node specified as argument does not exist in the DoublyLinkedList<T>,
        the method throws the InvalidOperationException.*/
        public INode<T> AddAfter(INode<T> after, T value)
        {
            if (after == null) throw new NullReferenceException();
            Node<T> node_current = after as Node<T>; //safecast
            //if node's neighbours are null or specified node is not in the list, throw an exception
            if (node_current.Previous == null || node_current.Next == null || Find(after.Value) == null)
            {
                throw new InvalidOperationException("The node referred as 'after' is no longer in the list");
            }
            return AddBetween(value, node_current, node_current.Next); //inserts value T after the specified node 'after'
            //throw new NotImplementedException();
        }
        /*Removes all nodes from the DoublyLinkedList<T>. Count is set to zero. For each of the nodes, links to the previous
         * and the next nodes must be nullified.*/
        public void Clear()
        {
            Head = null; //by pointing the node's head to null, all other nodes are nullified and the list is cleared.
            Count = 0;

            //throw new NotImplementedException();
        }
        /*Removes the specified node from the DoublyLinkedList<T>. If node is null, it throws the ArgumentNullException.
       If the node specified as argument does not exist in the DoublyLinkedList<T>, the method throws the
       InvalidOperationException.*/
        public void Remove(INode<T> node)
        { 
            if (node == null) throw new NullReferenceException(); //node is null, throw exception.

            Node<T> node_current = node as Node<T>; //safe cast

            if (Find(node.Value) == null) //Finds the node specified in the argument - if it returns null, throw exception
            {
                throw new InvalidOperationException("Node does not exist!");
            }
            if (node_current.Previous == null || node_current.Next == null) //if node's neighbours are null, throw exception.
            {
                throw new InvalidOperationException("The node referred as 'node' is no longer in the list");
            }
            //neighbouring nodes
            Node<T> predecessor = node_current.Previous;
            Node<T> successor = node_current.Next;
            //reassign neighbouring nodes so the node to be deleted has no more pointer references
            predecessor.Next = successor;
            successor.Previous = predecessor;
            

            Count--;
            node_current.Previous = null; //nullify to ensure there are not lingering pointers to the node
            node_current.Next = null;
            node_current = null;

        }
        /*Removes the node at the start of the DoublyLinkedList<T>. If the DoublyLinkedList<T> is empty, it throws
        InvalidOperationException.*/
        public void RemoveFirst()
        {
            if (Head == null) //if list is empty
                throw new InvalidOperationException("Node does not exist!");
            Remove(Head.Next);
        }
        /*Removes the node at the end of the DoublyLinkedList<T>. If the DoublyLinkedList<T> is empty, it throws
        InvalidOperationException.*/
        public void RemoveLast()
        {

            if (Head == null) //if list is empty
                throw new InvalidOperationException("Node does not exist!");
            Remove(Tail.Previous);
        }
    }
}
