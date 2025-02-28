using DataWise.Data.DbContexts.NonReleational.Enums;
using DataWise.Data.DbContexts.NonReleational.Models;
using DataWise.Data.Repositories.NonReleational;
using MongoDB.Driver;

namespace DataWise.Data.DbContexts.NonReleational;

public class DataSeeder(
    IMongoRepository<DataStructure, string> repository)
{
    public async Task SeedAsync()
    {
        var existingData = await repository.GetAllAsync();

        if (existingData.Any()) return;

        var arrayStructure = new DataStructure
        {
            Name = "Array",
            Description = "An array is a collection of items stored at contiguous memory locations, allowing fast access by index.",
            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTqGRZZ66Vfe_MSH8ihDb7PSQsPmy19AfxRoQ&s",
            Example = "[1, 2, 3, 4, 5]",
            AccessTimeComplexity = Complexity.Constant,
            InsertionTimeComplexity = Complexity.Linear,
            DeletionTimeComplexity = Complexity.Linear,
            CodeBlocks =
            [
                new CodeBlock
                {
                    Language = "Python",
                    Code = "array = [1, 2, 3, 4, 5]\nprint(array)"
                },
                new CodeBlock
                {
                    Language = "C#",
                    Code = "int[] array = { 1, 2, 3, 4, 5 };\nConsole.WriteLine(string.Join(\", \", array));"
                },
                new CodeBlock
                {
                    Language = "JavaScript",
                    Code = "const array = [1, 2, 3, 4, 5];\nconsole.log(array);"
                }
            ]
        };

        var linkedListStructure = new DataStructure
        {
            Name = "Linked List",
            Description = "A linked list is a linear data structure where elements are stored in nodes that contain a value and one or more references to other nodes. This structure allows for efficient insertion and deletion operations.",
            ImageUrl = "https://media.geeksforgeeks.org/wp-content/uploads/20220829110944/LLdrawio.png",
            Example = "1 -> 2 -> 3 -> null",
            AccessTimeComplexity = Complexity.Linear,
            InsertionTimeComplexity = Complexity.Constant,
            DeletionTimeComplexity = Complexity.Constant,
            SubTypes =
            [
                new DataStructureSubType
                {
                    Title = "Singly Linked List",
                    Description = "A singly linked list is a type of linked list in which each node points only to the next node.",
                    Differences = "Nodes only hold a reference to the next node, making the structure simpler and less memory-intensive, but it only allows one-way traversal.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "class Node:\n    def __init__(self, data):\n        self.data = data\n        self.next = None\n\nclass SinglyLinkedList:\n    def __init__(self):\n        self.head = None\n\n    def append(self, data):\n        if not self.head:\n            self.head = Node(data)\n            return\n        current = self.head\n        while current.next:\n            current = current.next\n        current.next = Node(data)\n\n    def print_list(self):\n        current = self.head\n        while current:\n            print(current.data, end=' -> ')\n            current = current.next\n        print('None')"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "public class Node {\n    public int Data;\n    public Node Next;\n    public Node(int data) {\n        Data = data;\n        Next = null;\n    }\n}\n\npublic class SinglyLinkedList {\n    public Node Head;\n\n    public void Append(int data) {\n        if (Head == null) {\n            Head = new Node(data);\n            return;\n        }\n        Node current = Head;\n        while (current.Next != null) {\n            current = current.Next;\n        }\n        current.Next = new Node(data);\n    }\n\n    public void PrintList() {\n        Node current = Head;\n        while (current != null) {\n            Console.Write(current.Data + \" -> \");\n            current = current.Next;\n        }\n        Console.WriteLine(\"null\");\n    }\n}"
                        }
                    ]
                },
                new DataStructureSubType
                {
                    Title = "Doubly Linked List",
                    Description = "A doubly linked list is a type of linked list where each node contains pointers to both the next and the previous nodes.",
                    Differences = "Nodes maintain two references, allowing traversal in both directions, which can simplify deletion and backward traversal at the expense of additional memory.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "class Node:\n    def __init__(self, data):\n        self.data = data\n        self.prev = None\n        self.next = None\n\nclass DoublyLinkedList:\n    def __init__(self):\n        self.head = None\n\n    def append(self, data):\n        new_node = Node(data)\n        if not self.head:\n            self.head = new_node\n            return\n        current = self.head\n        while current.next:\n            current = current.next\n        current.next = new_node\n        new_node.prev = current\n\n    def print_list(self):\n        current = self.head\n        while current:\n            print(current.data, end=' <-> ')\n            current = current.next\n        print('None')"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "public class DoublyNode {\n    public int Data;\n    public DoublyNode Prev;\n    public DoublyNode Next;\n\n    public DoublyNode(int data) {\n        Data = data;\n        Prev = null;\n        Next = null;\n    }\n}\n\npublic class DoublyLinkedList {\n    public DoublyNode Head;\n\n    public void Append(int data) {\n        var newNode = new DoublyNode(data);\n        if (Head == null) {\n            Head = newNode;\n            return;\n        }\n        var current = Head;\n        while (current.Next != null) {\n            current = current.Next;\n        }\n        current.Next = newNode;\n        newNode.Prev = current;\n    }\n\n    public void PrintList() {\n        var current = Head;\n        while (current != null) {\n            Console.Write(current.Data + \" <-> \");\n            current = current.Next;\n        }\n        Console.WriteLine(\"null\");\n    }\n}"
                        }
                    ]
                },
                new DataStructureSubType
                {
                    Title = "Circular Linked List",
                    Description = "A circular linked list is a linked list where the last node points back to the first node, creating a circle.",
                    Differences = "Unlike other linked lists, circular linked lists have no null reference at the end; the last node links back to the head, enabling continuous traversal.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {    
                            Language = "Python",
                            Code = "class Node:\n    def __init__(self, data):\n        self.data = data\n        self.next = None\n\nclass CircularLinkedList:\n    def __init__(self):\n        self.head = None\n\n    def append(self, data):\n        new_node = Node(data)\n        if not self.head:\n            self.head = new_node\n            new_node.next = self.head\n            return\n        current = self.head\n        while current.next != self.head:\n            current = current.next\n        current.next = new_node\n        new_node.next = self.head\n\n    def print_list(self, count=10):\n        current = self.head\n        for _ in range(count):\n            if current:\n                print(current.data, end=' -> ')\n                current = current.next\n        print('...')"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "public class CircularNode {\n    public int Data;\n    public CircularNode Next;\n\n    public CircularNode(int data) {\n        Data = data;\n        Next = null;\n    }\n}\n\npublic class CircularLinkedList {\n    public CircularNode Head;\n\n    public void Append(int data) {\n        var newNode = new CircularNode(data);\n        if (Head == null) {\n            Head = newNode;\n            newNode.Next = Head;\n            return;\n        }\n        var current = Head;\n        while (current.Next != Head) {\n            current = current.Next;\n        }\n        current.Next = newNode;\n        newNode.Next = Head;\n    }\n\n    public void PrintList(int count = 10) {\n        var current = Head;\n        for (int i = 0; i < count && current != null; i++) {\n            Console.Write(current.Data + \" -> \");\n            current = current.Next;\n        }\n        Console.WriteLine(\"...\");\n    }\n}"
                        }
                    ]
                }
            ]
        };

        var stackStructure = new DataStructure
        {
            Name = "Stack",
            Description = "A stack is a linear data structure that follows the Last In First Out (LIFO) principle. Elements are added and removed from the top of the stack.",
            ImageUrl = "https://cdn.programiz.com/sites/tutorial2program/files/stack.png",
            Example = "[Bottom] 1, 2, 3 [Top]",
            AccessTimeComplexity = Complexity.Linear,
            InsertionTimeComplexity = Complexity.Constant,
            DeletionTimeComplexity = Complexity.Constant,
            CodeBlocks =
            [
                new CodeBlock
                {
                    Language = "Python",
                    Code = "class Stack:\n    def __init__(self):\n        self.items = []\n\n    def push(self, item):\n        self.items.append(item)\n\n    def pop(self):\n        if not self.is_empty():\n            return self.items.pop()\n        return None\n\n    def peek(self):\n        if not self.is_empty():\n            return self.items[-1]\n        return None\n\n    def is_empty(self):\n        return len(self.items) == 0\n\n    def size(self):\n        return len(self.items)"
                },
                new CodeBlock
                {
                    Language = "C#",
                    Code = "public class Stack<T>\n{\n    private List<T> _elements = new List<T>();\n\n    public void Push(T item) => _elements.Add(item);\n\n    public T Pop()\n    {\n        if (_elements.Count == 0)\n            throw new InvalidOperationException(\"Stack is empty.\");\n        T item = _elements[_elements.Count - 1];\n        _elements.RemoveAt(_elements.Count - 1);\n        return item;\n    }\n\n    public T Peek()\n    {\n        if (_elements.Count == 0)\n            throw new InvalidOperationException(\"Stack is empty.\");\n        return _elements[_elements.Count - 1];\n    }\n\n    public bool IsEmpty => _elements.Count == 0;\n\n    public int Count => _elements.Count;\n}"
                }
            ]
        };

        var queueStructure = new DataStructure
        {
            Name = "Queue",
            Description = "A queue is a linear data structure that follows the First In First Out (FIFO) principle. Elements are enqueued at the rear and dequeued from the front.",
            ImageUrl = "https://media.geeksforgeeks.org/wp-content/uploads/20220816162225/Queue.png",
            Example = "[Front] 1, 2, 3, 4 [Rear]",
            AccessTimeComplexity = Complexity.Linear,
            InsertionTimeComplexity = Complexity.Constant,
            DeletionTimeComplexity = Complexity.Constant,
            SubTypes =
            [
                new DataStructureSubType
                {
                    Title = "Normal Queue",
                    Description = "A normal queue follows the standard FIFO principle where elements are inserted at the rear and removed from the front.",
                    Differences = "It maintains order as elements are served in the exact sequence they were added.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "from collections import deque\n\nclass Queue:\n    def __init__(self):\n        self.items = deque()\n\n    def enqueue(self, item):\n        self.items.append(item)\n\n    def dequeue(self):\n        if not self.is_empty():\n            return self.items.popleft()\n        return None\n\n    def is_empty(self):\n        return len(self.items) == 0\n\n    def size(self):\n        return len(self.items)"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "using System;\nusing System.Collections.Generic;\n\npublic class Queue<T>\n{\n    private LinkedList<T> _elements = new LinkedList<T>();\n\n    public void Enqueue(T item) => _elements.AddLast(item);\n\n    public T Dequeue()\n    {\n        if (_elements.Count == 0)\n            throw new InvalidOperationException(\"Queue is empty.\");\n        T value = _elements.First.Value;\n        _elements.RemoveFirst();\n        return value;\n    }\n\n    public bool IsEmpty => _elements.Count == 0;\n\n    public int Count => _elements.Count;\n}"
                        }
                    ]
                },
                new DataStructureSubType
                {
                    Title = "Circular Queue",
                    Description = "A circular queue is a queue where the last position connects back to the first position, forming a circle.",
                    Differences = "Unlike a normal queue, it efficiently utilizes memory by reusing vacant slots after dequeuing.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "class CircularQueue:\n    def __init__(self, size):\n        self.queue = [None] * size\n        self.size = size\n        self.front = self.rear = -1\n\n    def enqueue(self, item):\n        if (self.rear + 1) % self.size == self.front:\n            print(\"Queue is full\")\n            return\n        if self.front == -1:\n            self.front = 0\n        self.rear = (self.rear + 1) % self.size\n        self.queue[self.rear] = item\n\n    def dequeue(self):\n        if self.front == -1:\n            print(\"Queue is empty\")\n            return None\n        value = self.queue[self.front]\n        if self.front == self.rear:\n            self.front = self.rear = -1\n        else:\n            self.front = (self.front + 1) % self.size\n        return value"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "using System;\n\npublic class CircularQueue\n{\n    private int[] _queue;\n    private int _front, _rear, _size, _capacity;\n\n    public CircularQueue(int capacity)\n    {\n        _capacity = capacity;\n        _queue = new int[capacity];\n        _front = _rear = -1;\n        _size = 0;\n    }\n\n    public void Enqueue(int item)\n    {\n        if (_size == _capacity)\n            throw new InvalidOperationException(\"Queue is full\");\n        if (_front == -1)\n            _front = 0;\n        _rear = (_rear + 1) % _capacity;\n        _queue[_rear] = item;\n        _size++;\n    }\n\n    public int Dequeue()\n    {\n        if (_size == 0)\n            throw new InvalidOperationException(\"Queue is empty\");\n        int value = _queue[_front];\n        _front = (_front + 1) % _capacity;\n        _size--;\n        return value;\n    }"
                        }
                    ]
                },
                new DataStructureSubType
                {
                    Title = "Priority Queue",
                    Description = "A priority queue is a queue where elements are dequeued based on priority rather than order of insertion.",
                    Differences = "Unlike normal queues, elements with higher priority are served first, irrespective of their insertion order.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "import heapq\n\nclass PriorityQueue:\n    def __init__(self):\n        self.queue = []\n\n    def enqueue(self, item, priority):\n        heapq.heappush(self.queue, (priority, item))\n\n    def dequeue(self):\n        if self.queue:\n            return heapq.heappop(self.queue)[1]\n        return None"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "using System;\nusing System.Collections.Generic;\n\npublic class PriorityQueue<T>\n{\n    private SortedDictionary<int, Queue<T>> _elements = new SortedDictionary<int, Queue<T>>();\n\n    public void Enqueue(T item, int priority)\n    {\n        if (!_elements.ContainsKey(priority))\n            _elements[priority] = new Queue<T>();\n        _elements[priority].Enqueue(item);\n    }\n\n    public T Dequeue()\n    {\n        if (_elements.Count == 0)\n            throw new InvalidOperationException(\"Queue is empty\");\n        int minPriority = _elements.Keys.Min();\n        T item = _elements[minPriority].Dequeue();\n        if (_elements[minPriority].Count == 0)\n            _elements.Remove(minPriority);\n        return item;\n    }\n}"
                        }
                    ]
                },
                new DataStructureSubType
                {
                    Title = "Double-Ended Queue",
                    Description = "A double-ended queue (deque) supports insertion and removal of elements from both the front and the rear.",
                    Differences = "Unlike normal queues or stacks, a deque allows operations at both ends with efficient performance.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "from collections import deque\n\nclass Deque:\n    def __init__(self):\n        self.items = deque()\n\n    def append_front(self, item):\n        self.items.appendleft(item)\n\n    def append_rear(self, item):\n        self.items.append(item)\n\n    def pop_front(self):\n        if self.items:\n            return self.items.popleft()\n        return None\n\n    def pop_rear(self):\n        if self.items:\n            return self.items.pop()\n        return None\n\n    def is_empty(self):\n        return len(self.items) == 0\n\n    def size(self):\n        return len(self.items)"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "using System;\nusing System.Collections.Generic;\n\npublic class Deque<T>\n{\n    private LinkedList<T> _items = new LinkedList<T>();\n\n    public void AppendFront(T item) => _items.AddFirst(item);\n\n    public void AppendRear(T item) => _items.AddLast(item);\n\n    public T PopFront()\n    {\n        if (_items.Count == 0)\n            throw new InvalidOperationException(\"Deque is empty.\");\n        T value = _items.First.Value;\n        _items.RemoveFirst();\n        return value;\n    }\n\n    public T PopRear()\n    {\n        if (_items.Count == 0)\n            throw new InvalidOperationException(\"Deque is empty.\");\n        T value = _items.Last.Value;\n        _items.RemoveLast();\n        return value;\n    }\n\n    public bool IsEmpty => _items.Count == 0;\n\n    public int Count => _items.Count;\n}"
                        }
                    ]
                },
            ]
        };

        var hashTableStructure = new DataStructure
        {
            Name = "Hash Table",
            Description = "A hash table is a data structure that maps keys to values using a hash function, enabling fast insertion, deletion, and lookup operations.",
            ImageUrl = "https://i0.wp.com/technicalsand.com/wp-content/uploads/2020/08/Hashing-in-data-structure-1.png?fit=967%2C578&ssl=1",
            Example = "{ 'key1': 'value1', 'key2': 'value2' }",
            AccessTimeComplexity = Complexity.Constant,
            InsertionTimeComplexity = Complexity.Constant,
            DeletionTimeComplexity = Complexity.Constant,
            CodeBlocks =
            [
                new CodeBlock
                {
                    Language = "Python",
                    Code = "hash_table = {}\n\n# Insert\nhash_table['key1'] = 'value1'\n\n# Access\nprint(hash_table['key1'])\n\n# Delete\ndel hash_table['key1']"
                },
                new CodeBlock
                {
                    Language = "C#",
                    Code = "using System;\nusing System.Collections.Generic;\n\nvar hashTable = new Dictionary<string, string>();\n\n// Insert\nhashTable[\"key1\"] = \"value1\";\n\n// Access\nConsole.WriteLine(hashTable[\"key1\"]);\n\n// Delete\nhashTable.Remove(\"key1\");"
                }
            ]
        };

        var treeStructure = new DataStructure
        {
            Name = "Tree",
            Description = "A tree is a hierarchical data structure consisting of nodes connected by edges. It is used to represent hierarchical relationships.",
            ImageUrl = "https://miro.medium.com/v2/resize:fit:677/1*Z89j_NoDx9HkFcPHy3rPZg.png",
            Example = "Root -> Child1, Child2, ...",
            AccessTimeComplexity = Complexity.Logarithmic,
            InsertionTimeComplexity = Complexity.Logarithmic,
            DeletionTimeComplexity = Complexity.Logarithmic,
            SubTypes =
            [
                new DataStructureSubType
                {
                    Title = "Binary Tree",
                    Description = "A binary tree is a tree data structure in which each node has at most two children.",
                    Differences = "It serves as the base structure for many specialized trees.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "class Node:\n    def __init__(self, data):\n        self.data = data\n        self.left = None\n        self.right = None"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "public class Node {\n    public int Data;\n    public Node Left, Right;\n    public Node(int data) {\n        Data = data;\n        Left = Right = null;\n    }\n}"
                        }
                    ]
                },
                new DataStructureSubType
                {
                    Title = "Binary Search Tree (BST)",
                    Description = "A BST is a binary tree in which for every node, the left subtree contains only nodes with keys less than the node’s key and the right subtree only nodes with keys greater than the node’s key.",
                    Differences = "Provides ordered data storage with efficient search, insertion, and deletion operations.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "class BSTNode:\n    def __init__(self, data):\n        self.data = data\n        self.left = None\n        self.right = None\n\ndef insert(root, data):\n    if root is None:\n        return BSTNode(data)\n    if data < root.data:\n        root.left = insert(root.left, data)\n    else:\n        root.right = insert(root.right, data)\n    return root"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "public class BSTNode {\n    public int Data;\n    public BSTNode Left, Right;\n    public BSTNode(int data) {\n        Data = data;\n        Left = Right = null;\n    }\n}\n\npublic BSTNode Insert(BSTNode root, int data) {\n    if (root == null) return new BSTNode(data);\n    if (data < root.Data)\n        root.Left = Insert(root.Left, data);\n    else\n        root.Right = Insert(root.Right, data);\n    return root;\n}"
                        }
                    ]
                },
                new DataStructureSubType
                {
                    Title = "AVL Tree",
                    Description = "An AVL tree is a self-balancing BST where the heights of the two child subtrees of any node differ by at most one.",
                    Differences = "It automatically rebalances after insertions and deletions to ensure O(log n) operations.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "class AVLNode:\n    def __init__(self, key):\n        self.key = key\n        self.left = None\n        self.right = None\n        self.height = 1\n\ndef get_height(node):\n    return node.height if node else 0\n\ndef get_balance(node):\n    return get_height(node.left) - get_height(node.right) if node else 0\n\ndef right_rotate(y):\n    x = y.left\n    T2 = x.right\n    x.right = y\n    y.left = T2\n    y.height = 1 + max(get_height(y.left), get_height(y.right))\n    x.height = 1 + max(get_height(x.left), get_height(x.right))\n    return x\n\ndef left_rotate(x):\n    y = x.right\n    T2 = y.left\n    y.left = x\n    x.right = T2\n    x.height = 1 + max(get_height(x.left), get_height(x.right))\n    y.height = 1 + max(get_height(y.left), get_height(y.right))\n    return y\n\ndef insert_avl(root, key):\n    if not root:\n        return AVLNode(key)\n    elif key < root.key:\n        root.left = insert_avl(root.left, key)\n    else:\n        root.right = insert_avl(root.right, key)\n    root.height = 1 + max(get_height(root.left), get_height(root.right))\n    balance = get_balance(root)\n    if balance > 1 and key < root.left.key:\n        return right_rotate(root)\n    if balance < -1 and key > root.right.key:\n        return left_rotate(root)\n    if balance > 1 and key > root.left.key:\n        root.left = left_rotate(root.left)\n        return right_rotate(root)\n    if balance < -1 and key < root.right.key:\n        root.right = right_rotate(root.right)\n        return left_rotate(root)\n    return root"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "public class AVLNode {\n    public int Key;\n    public AVLNode Left, Right;\n    public int Height;\n    public AVLNode(int key) {\n        Key = key;\n        Height = 1;\n    }\n}\n\npublic int GetHeight(AVLNode node) => node == null ? 0 : node.Height;\n\npublic int GetBalance(AVLNode node) => node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);\n\npublic AVLNode RightRotate(AVLNode y) {\n    AVLNode x = y.Left;\n    AVLNode T2 = x.Right;\n    x.Right = y;\n    y.Left = T2;\n    y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;\n    x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;\n    return x;\n}\n\npublic AVLNode LeftRotate(AVLNode x) {\n    AVLNode y = x.Right;\n    AVLNode T2 = y.Left;\n    y.Left = x;\n    x.Right = T2;\n    x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;\n    y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;\n    return y;\n}\n\npublic AVLNode InsertAVL(AVLNode node, int key) {\n    if (node == null)\n        return new AVLNode(key);\n    if (key < node.Key)\n        node.Left = InsertAVL(node.Left, key);\n    else\n        node.Right = InsertAVL(node.Right, key);\n    node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;\n    int balance = GetBalance(node);\n    if (balance > 1 && key < node.Left.Key)\n        return RightRotate(node);\n    if (balance < -1 && key > node.Right.Key)\n        return LeftRotate(node);\n    if (balance > 1 && key > node.Left.Key) {\n        node.Left = LeftRotate(node.Left);\n        return RightRotate(node);\n    }\n    if (balance < -1 && key < node.Right.Key) {\n        node.Right = RightRotate(node.Right);\n        return LeftRotate(node);\n    }\n    return node;\n}"
                        }
                    ]
                },
                new DataStructureSubType
                {
                    Title = "Red-Black Tree",
                    Description = "A Red-Black Tree is a self-balancing BST that uses node colors (red or black) to ensure balance during insertions and deletions.",
                    Differences = "It offers good worst-case performance and is widely used in many libraries and systems.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "class RBNode:\n    def __init__(self, key, color='red'):\n        self.key = key\n        self.color = color\n        self.left = None\n        self.right = None\n        self.parent = None\n\ndef insert_rb(root, key):\n    if root is None:\n        return RBNode(key, color='black')  # Root is always black\n    if key < root.key:\n        root.left = insert_rb(root.left, key)\n    else:\n        root.right = insert_rb(root.right, key)\n    return root"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "public enum NodeColor { Red, Black }\n\npublic class RBNode {\n    public int Key;\n    public NodeColor Color;\n    public RBNode Left, Right, Parent;\n    public RBNode(int key) {\n        Key = key;\n        Color = NodeColor.Red;\n    }\n}\npublic RBNode InsertRB(RBNode root, int key) {\n    if (root == null) {\n        var newNode = new RBNode(key);\n        newNode.Color = NodeColor.Black;\n        return newNode;\n    }\n    if (key < root.Key)\n        root.Left = InsertRB(root.Left, key);\n    else\n        root.Right = InsertRB(root.Right, key);\n    return root;\n}"
                        }
                    ]
                },
                new DataStructureSubType
                {
                    Title = "B-Tree",
                    Description = "A B-Tree is a self-balancing tree data structure that maintains sorted data and allows searches, sequential access, insertions, and deletions in logarithmic time.",
                    Differences = "It is optimized for systems that read and write large blocks of data, such as databases and filesystems.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "class BTreeNode:\n    def __init__(self, t, leaf=False):\n        self.t = t\n        self.leaf = leaf\n        self.keys = []\n        self.children = []"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "public class BTreeNode {\n    public int[] Keys;\n    public int T;  // Minimum degree\n    public BTreeNode[] Children;\n    public int KeyCount;\n    public bool IsLeaf;\n\n    public BTreeNode(int t, bool isLeaf) {\n        T = t;\n        IsLeaf = isLeaf;\n        Keys = new int[2 * t - 1];\n        Children = new BTreeNode[2 * t];\n        KeyCount = 0;\n    }\n}"
                        }
                    ]
                },
                new DataStructureSubType
                {
                    Title = "PreFix Tree (Trie)",
                    Description = "A prefix tree, or trie, is a tree-like data structure used to efficiently store and retrieve keys in a dataset of strings.",
                    Differences = "It excels at prefix-based searches, making it useful for autocomplete and spell-checking.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "class TrieNode:\n    def __init__(self):\n        self.children = {}\n        self.is_end_of_word = False\n\nclass Trie:\n    def __init__(self):\n        self.root = TrieNode()\n\n    def insert(self, word):\n        current = self.root\n        for char in word:\n            if char not in current.children:\n                current.children[char] = TrieNode()\n            current = current.children[char]\n        current.is_end_of_word = True\n\n    def search(self, word):\n        current = self.root\n        for char in word:\n            if char not in current.children:\n                return False\n            current = current.children[char]\n        return current.is_end_of_word"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "using System;\nusing System.Collections.Generic;\n\npublic class TrieNode {\n    public Dictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();\n    public bool IsEndOfWord;\n}\n\npublic class Trie {\n    private TrieNode root = new TrieNode();\n\n    public void Insert(string word) {\n        var current = root;\n        foreach (var ch in word) {\n            if (!current.Children.ContainsKey(ch))\n                current.Children[ch] = new TrieNode();\n            current = current.Children[ch];\n        }\n        current.IsEndOfWord = true;\n    }\n\n    public bool Search(string word) {\n        var current = root;\n        foreach (var ch in word) {\n            if (!current.Children.ContainsKey(ch))\n                return false;\n            current = current.Children[ch];\n        }\n        return current.IsEndOfWord;\n    }\n}"
                        }
                    ]
                }
            ]
        };

        var heapStructure = new DataStructure
        {
            Name = "Heap",
            Description = "A heap is a specialized tree-based data structure that satisfies the heap property. In a max heap, every parent node is greater than or equal to its children, while in a min heap, every parent node is less than or equal to its children.",
            ImageUrl = "https://media.geeksforgeeks.org/wp-content/cdn-uploads/20221220165711/MinHeapAndMaxHeap1.png",
            Example = "Max Heap: [100, 50, 30, ...] | Min Heap: [10, 20, 30, ...]",
            AccessTimeComplexity = Complexity.Logarithmic,
            InsertionTimeComplexity = Complexity.Logarithmic,
            DeletionTimeComplexity = Complexity.Logarithmic,
            SubTypes =
            [
                new DataStructureSubType
                {
                    Title = "Max Heap",
                    Description = "A max heap is a complete binary tree where each node is greater than or equal to its children. It allows efficient retrieval of the maximum element.",
                    Differences = "It ensures that the largest element is always at the root.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "import heapq\n\nclass MaxHeap:\n    def __init__(self):\n        self.heap = []\n\n    def push(self, item):\n        heapq.heappush(self.heap, -item)\n\n    def pop(self):\n        return -heapq.heappop(self.heap)"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "using System;\nusing System.Collections.Generic;\n\npublic class MaxHeap {\n    private List<int> heap = new List<int>();\n\n    public void Push(int item) {\n        heap.Add(item);\n        int i = heap.Count - 1;\n        while (i > 0) {\n            int parent = (i - 1) / 2;\n            if (heap[parent] >= heap[i]) break;\n            int temp = heap[parent];\n            heap[parent] = heap[i];\n            heap[i] = temp;\n            i = parent;\n        }\n    }\n\n    public int Pop() {\n        if (heap.Count == 0) throw new InvalidOperationException(\"Heap is empty.\");\n        int root = heap[0];\n        heap[0] = heap[heap.Count - 1];\n        heap.RemoveAt(heap.Count - 1);\n        Heapify(0);\n        return root;\n    }\n\n    private void Heapify(int i) {\n        int left = 2 * i + 1;\n        int right = 2 * i + 2;\n        int largest = i;\n        if (left < heap.Count && heap[left] > heap[largest]) largest = left;\n        if (right < heap.Count && heap[right] > heap[largest]) largest = right;\n        if (largest != i) {\n            int temp = heap[i];\n            heap[i] = heap[largest];\n            heap[largest] = temp;\n            Heapify(largest);\n        }\n    }\n}"
                        }
                    ]
                },
                new DataStructureSubType
                {
                    Title = "Min Heap",
                    Description = "A min heap is a complete binary tree where each node is less than or equal to its children. It allows efficient retrieval of the minimum element.",
                    Differences = "It ensures that the smallest element is always at the root.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "import heapq\n\nclass MinHeap:\n    def __init__(self):\n        self.heap = []\n\n    def push(self, item):\n        heapq.heappush(self.heap, item)\n\n    def pop(self):\n        return heapq.heappop(self.heap)"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "using System;\nusing System.Collections.Generic;\n\npublic class MinHeap {\n    private List<int> heap = new List<int>();\n\n    public void Push(int item) {\n        heap.Add(item);\n        int i = heap.Count - 1;\n        while (i > 0) {\n            int parent = (i - 1) / 2;\n            if (heap[parent] <= heap[i]) break;\n            int temp = heap[parent];\n            heap[parent] = heap[i];\n            heap[i] = temp;\n            i = parent;\n        }\n    }\n\n    public int Pop() {\n        if (heap.Count == 0) throw new InvalidOperationException(\"Heap is empty.\");\n        int root = heap[0];\n        heap[0] = heap[heap.Count - 1];\n        heap.RemoveAt(heap.Count - 1);\n        Heapify(0);\n        return root;\n    }\n\n    private void Heapify(int i) {\n        int left = 2 * i + 1;\n        int right = 2 * i + 2;\n        int smallest = i;\n        if (left < heap.Count && heap[left] < heap[smallest]) smallest = left;\n        if (right < heap.Count && heap[right] < heap[smallest]) smallest = right;\n        if (smallest != i) {\n            int temp = heap[i];\n            heap[i] = heap[smallest];\n            heap[smallest] = temp;\n            Heapify(smallest);\n        }\n    }\n}"
                        }
                    ]
                }
            ]
        };

        var graphStructure = new DataStructure
        {
            Name = "Graph",
            Description = "A graph is a collection of nodes (vertices) and edges that connect them. Graphs can represent networks, relationships, or connections between entities.",
            ImageUrl = "https://www.boardinfinity.com/blog/content/images/2023/01/Graphs-in-DSA.png",
            Example = "Nodes: {A, B, C}; Edges: {(A, B), (B, C)}",
            AccessTimeComplexity = Complexity.Unknown,
            InsertionTimeComplexity = Complexity.Unknown,
            DeletionTimeComplexity = Complexity.Unknown,
            SubTypes =
            [
                new DataStructureSubType
                {
                    Title = "Directed Graph",
                    Description = "A directed graph (digraph) is a graph where each edge has a direction, indicating a one-way relationship between two nodes.",
                    Differences = "Edges have direction; an edge from A to B does not imply an edge from B to A.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "class DirectedGraph:\n    def __init__(self):\n        self.adj_list = {}\n\n    def add_edge(self, u, v):\n        if u not in self.adj_list:\n            self.adj_list[u] = []\n        self.adj_list[u].append(v)"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "using System;\nusing System.Collections.Generic;\n\npublic class DirectedGraph {\n    public Dictionary<string, List<string>> AdjList { get; set; } = new Dictionary<string, List<string>>();\n\n    public void AddEdge(string u, string v) {\n        if (!AdjList.ContainsKey(u))\n            AdjList[u] = new List<string>();\n        AdjList[u].Add(v);\n    }\n}"
                        }
                    ]
                },
                new DataStructureSubType
                {
                    Title = "Undirected Graph",
                    Description = "An undirected graph is a graph where edges have no direction, representing bidirectional relationships.",
                    Differences = "Edges are two-way; an edge between A and B implies a connection from A to B and from B to A.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "class UndirectedGraph:\n    def __init__(self):\n        self.adj_list = {}\n\n    def add_edge(self, u, v):\n        if u not in self.adj_list:\n            self.adj_list[u] = []\n        if v not in self.adj_list:\n            self.adj_list[v] = []\n        self.adj_list[u].append(v)\n        self.adj_list[v].append(u)"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "using System;\nusing System.Collections.Generic;\n\npublic class UndirectedGraph {\n    public Dictionary<string, List<string>> AdjList { get; set; } = new Dictionary<string, List<string>>();\n\n    public void AddEdge(string u, string v) {\n        if (!AdjList.ContainsKey(u))\n            AdjList[u] = new List<string>();\n        if (!AdjList.ContainsKey(v))\n            AdjList[v] = new List<string>();\n        AdjList[u].Add(v);\n        AdjList[v].Add(u);\n    }\n}"
                        }
                    ]
                },
                new DataStructureSubType
                {
                    Title = "Weighted Graph",
                    Description = "A weighted graph is a graph in which each edge is assigned a weight, representing cost, distance, or another metric.",
                    Differences = "Edges carry weights which influence algorithms like shortest path and minimum spanning tree.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "class WeightedGraph:\n    def __init__(self):\n        self.adj_list = {}\n\n    def add_edge(self, u, v, weight):\n        if u not in self.adj_list:\n            self.adj_list[u] = []\n        self.adj_list[u].append((v, weight))"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "using System;\nusing System.Collections.Generic;\n\npublic class WeightedGraph {\n    public Dictionary<string, List<(string, int)>> AdjList { get; set; } = new Dictionary<string, List<(string, int)>>();\n\n    public void AddEdge(string u, string v, int weight) {\n        if (!AdjList.ContainsKey(u))\n            AdjList[u] = new List<(string, int)>();\n        AdjList[u].Add((v, weight));\n    }\n}"
                        }
                    ]
                },
                new DataStructureSubType
                {
                    Title = "Unweighted Graph",
                    Description = "An unweighted graph is a graph where edges do not have associated weights. It represents simple connections between nodes.",
                    Differences = "All edges are considered equal; there is no cost associated with an edge.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "class UnweightedGraph:\n    def __init__(self):\n        self.adj_list = {}\n\n    def add_edge(self, u, v):\n        if u not in self.adj_list:\n            self.adj_list[u] = []\n        self.adj_list[u].append(v)"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "using System;\nusing System.Collections.Generic;\n\npublic class UnweightedGraph {\n    public Dictionary<string, List<string>> AdjList { get; set; } = new Dictionary<string, List<string>>();\n\n    public void AddEdge(string u, string v) {\n        if (!AdjList.ContainsKey(u))\n            AdjList[u] = new List<string>();\n        AdjList[u].Add(v);\n    }\n}"
                        }
                    ]
                }
            ]
        };

        var setStructure = new DataStructure
        {
            Name = "Set",
            Description = "A set is an unordered collection of unique elements that provides fast lookup, insertion, and deletion operations.",
            ImageUrl = "https://media.geeksforgeeks.org/wp-content/uploads/20230302151935/s.png",
            Example = "{1, 2, 3}",
            AccessTimeComplexity = Complexity.Constant,
            InsertionTimeComplexity = Complexity.Constant,
            DeletionTimeComplexity = Complexity.Constant,
            SubTypes =
            [
                new DataStructureSubType
                {
                    Title = "Hash Set",
                    Description = "A hash set uses a hash table to store elements, offering O(1) average time complexity for insertion, deletion, and search operations.",
                    Differences = "Unordered with fast operations on average.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "my_set = set()\nmy_set.add(1)\nmy_set.add(2)\nmy_set.add(3)\nprint(my_set)"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "using System;\nusing System.Collections.Generic;\n\nvar hashSet = new HashSet<int>();\nhashSet.Add(1);\nhashSet.Add(2);\nhashSet.Add(3);\n\nforeach (var item in hashSet)\n    Console.WriteLine(item);"
                        }
                    ]
                },
                new DataStructureSubType
                {
                    Title = "Tree Set",
                    Description = "A tree set stores elements in a sorted order, typically implemented using self-balancing binary search trees.",
                    Differences = "Ordered and allows in-order traversal, but with slightly higher overhead compared to hash sets.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "class TreeSet:\n    def __init__(self):\n        self.data = set()\n\n    def add(self, item):\n        self.data.add(item)\n\n    def remove(self, item):\n        self.data.remove(item)\n\n    def __iter__(self):\n        return iter(sorted(self.data))"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "using System;\nusing System.Collections.Generic;\n\nvar treeSet = new SortedSet<int>();\ntreeSet.Add(3);\ntreeSet.Add(1);\ntreeSet.Add(2);\n\nforeach (var item in treeSet)\n    Console.WriteLine(item);"
                        }
                    ]
                }
            ]
        };

        var mapStructure = new DataStructure
        {
            Name = "Map",
            Description = "A map is an associative array that stores key-value pairs, allowing fast lookup, insertion, and deletion operations.",
            ImageUrl = "https://media.geeksforgeeks.org/wp-content/uploads/20230306152011/mp1.png",
            Example = "{ 'key': 'value', 'anotherKey': 'anotherValue' }",
            AccessTimeComplexity = Complexity.Constant,
            InsertionTimeComplexity = Complexity.Constant,
            DeletionTimeComplexity = Complexity.Constant,
            SubTypes =
            [
                new DataStructureSubType
                {
                    Title = "Hash Map",
                    Description = "A hash map stores key-value pairs using a hash function, offering average-case constant time complexity for most operations.",
                    Differences = "Unordered structure with very fast operations on average.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "my_map = {}\nmy_map['key'] = 'value'\nmy_map['anotherKey'] = 'anotherValue'\nprint(my_map)"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "using System;\nusing System.Collections.Generic;\n\nvar hashMap = new Dictionary<string, string>();\nhashMap[\"key\"] = \"value\";\nhashMap[\"anotherKey\"] = \"anotherValue\";\n\nforeach(var kvp in hashMap)\n    Console.WriteLine($\"{kvp.Key}: {kvp.Value}\");"
                        }
                    ]
                },
                new DataStructureSubType
                {
                    Title = "Tree Map",
                    Description = "A tree map stores key-value pairs in sorted order, typically implemented using a self-balancing binary search tree.",
                    Differences = "Maintains keys in sorted order, enabling in-order traversal and range queries at the expense of slightly slower operations.",
                    CodeBlocks =
                    [
                        new CodeBlock
                        {
                            Language = "Python",
                            Code = "from sortedcontainers import SortedDict\n\ntree_map = SortedDict()\ntree_map['key'] = 'value'\ntree_map['anotherKey'] = 'anotherValue'\nprint(tree_map)"
                        },
                        new CodeBlock
                        {
                            Language = "C#",
                            Code = "using System;\nusing System.Collections.Generic;\n\nvar treeMap = new SortedDictionary<string, string>();\ntreeMap[\"key\"] = \"value\";\ntreeMap[\"anotherKey\"] = \"anotherValue\";\n\nforeach(var kvp in treeMap)\n    Console.WriteLine($\"{kvp.Key}: {kvp.Value}\");"
                        }
                    ]
                }
            ]
        };

        await repository.AddRangeAsync([
            arrayStructure, linkedListStructure, stackStructure,
            queueStructure, hashTableStructure, treeStructure,
            heapStructure, graphStructure, setStructure,
            mapStructure]);
    }
}
