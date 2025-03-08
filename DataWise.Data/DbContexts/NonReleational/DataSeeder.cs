using MongoDB.Driver;
using DataWise.Data.DbContexts.NonReleational.Enums;
using DataWise.Data.DbContexts.NonReleational.Models;
using NR_REPOSITORIES = DataWise.Data.Repositories.NonReleational;

namespace DataWise.Data.DbContexts.NonReleational;

/// <summary>
/// Responsible for seeding initial data for data structures and algorithms.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DataSeeder"/> class.
/// </remarks>
/// <param name="dsRepository">The repository for data structures.</param>
/// <param name="aRepository">The repository for algorithms.</param>
public class DataSeeder(
    NR_REPOSITORIES.IMongoRepository<DataStructure, string> dsRepository,
    NR_REPOSITORIES.IMongoRepository<Algorithm, string> aRepository)
{

    /// <summary>
    /// Seeds all necessary data for data structures and algorithms.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SeedAllAsync()
    {
        await SeedDataStructuresAsync();

        await SeedAlgorithmsAsync();
    }

    private async Task SeedDataStructuresAsync()
    {
        var existingData = await dsRepository.GetAllAsync();

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

        await dsRepository.AddRangeAsync([
            arrayStructure, linkedListStructure, stackStructure,
            queueStructure, hashTableStructure, treeStructure,
            heapStructure, graphStructure, setStructure,
            mapStructure]);
    }

    private async Task SeedAlgorithmsAsync()
    {
        var existingData = await aRepository.GetAllAsync();

        if (existingData.Any()) return;

        #region Sorting

        var sortingCategory = new AlgorithmCategory
        {
            Name = "Sorting",
            Description = "Algorithms that arrange data in a specific order."
        };

        var sortingAlgorithms = new Algorithm[]
        {
            new ()
            {
                Name = "Bubble",
                Description = "Bubble sort repeatedly steps through the list, compares adjacent elements, and swaps them if needed.",
                Complexity = Complexity.Quadratic,
                UseCases = ["Educational", "Small datasets"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def bubble_sort(arr):\n    n = len(arr)\n    for i in range(n):\n        for j in range(0, n - i - 1):\n            if arr[j] > arr[j + 1]:\n                arr[j], arr[j + 1] = arr[j + 1], arr[j]\n    return arr" },
                    new CodeBlock { Language = "C#", Code = "public void BubbleSort(int[] arr)\n{\n    int n = arr.Length;\n    for (int i = 0; i < n - 1; i++)\n    {\n        for (int j = 0; j < n - i - 1; j++)\n        {\n            if (arr[j] > arr[j + 1])\n            {\n                int temp = arr[j];\n                arr[j] = arr[j + 1];\n                arr[j + 1] = temp;\n            }\n        }\n    }\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function bubbleSort(arr) {\n    let n = arr.length;\n    for (let i = 0; i < n - 1; i++) {\n        for (let j = 0; j < n - i - 1; j++) {\n            if (arr[j] > arr[j + 1]) {\n                let temp = arr[j];\n                arr[j] = arr[j + 1];\n                arr[j + 1] = temp;\n            }\n        }\n    }\n    return arr;\n}" },
                ],
                Category = sortingCategory
            },
            new ()
            {
                Name = "Selection",
                Description = "Selection sort repeatedly finds the minimum element from the unsorted portion and places it at the beginning.",
                Complexity = Complexity.Quadratic,
                UseCases = ["Simple sorting", "Educational"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def selection_sort(arr):\n    n = len(arr)\n    for i in range(n):\n        min_index = i\n        for j in range(i + 1, n):\n            if arr[j] < arr[min_index]:\n                min_index = j\n        arr[i], arr[min_index] = arr[min_index], arr[i]\n    return arr" },
                    new CodeBlock { Language = "C#", Code = "public void SelectionSort(int[] arr)\n{\n    int n = arr.Length;\n    for (int i = 0; i < n; i++)\n    {\n        int minIndex = i;\n        for (int j = i + 1; j < n; j++)\n        {\n            if (arr[j] < arr[minIndex])\n            {\n                minIndex = j;\n            }\n        }\n        int temp = arr[i];\n        arr[i] = arr[minIndex];\n        arr[minIndex] = temp;\n    }\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function selectionSort(arr) {\n    let n = arr.length;\n    for (let i = 0; i < n; i++) {\n        let minIndex = i;\n        for (let j = i + 1; j < n; j++) {\n            if (arr[j] < arr[minIndex]) {\n                minIndex = j;\n            }\n        }\n        let temp = arr[i];\n        arr[i] = arr[minIndex];\n        arr[minIndex] = temp;\n    }\n    return arr;\n}" }
                ],
                Category = sortingCategory
            },
            new ()
            {
                Name = "Insertion",
                Description = "Insertion sort builds the sorted array one element at a time by inserting each element into its proper position.",
                Complexity = Complexity.Quadratic,
                UseCases = ["Nearly sorted arrays", "Small datasets"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def insertion_sort(arr):\n    for i in range(1, len(arr)):\n        key = arr[i]\n        j = i - 1\n        while j >= 0 and arr[j] > key:\n            arr[j + 1] = arr[j]\n            j -= 1\n        arr[j + 1] = key\n    return arr" },
                    new CodeBlock { Language = "C#", Code = "public void InsertionSort(int[] arr)\n{\n    int n = arr.Length;\n    for (int i = 1; i < n; i++)\n    {\n        int key = arr[i];\n        int j = i - 1;\n        while (j >= 0 && arr[j] > key)\n        {\n            arr[j + 1] = arr[j];\n            j--;\n        }\n        arr[j + 1] = key;\n    }\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function insertionSort(arr) {\n    for (let i = 1; i < arr.length; i++) {\n        let key = arr[i];\n        let j = i - 1;\n        while (j >= 0 && arr[j] > key) {\n            arr[j + 1] = arr[j];\n            j--;\n        }\n        arr[j + 1] = key;\n    }\n    return arr;\n}" }
                ],
                Category = sortingCategory
            },
            new ()
            {
                Name = "Quick",
                Description = "Quick sort uses a divide-and-conquer approach by selecting a pivot and partitioning the array around it.",
                Complexity = Complexity.Linearithmic,
                UseCases = ["Large datasets", "General-purpose sorting"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def quick_sort(arr):\n    if len(arr) <= 1:\n        return arr\n    pivot = arr[len(arr) // 2]\n    left = [x for x in arr if x < pivot]\n    middle = [x for x in arr if x == pivot]\n    right = [x for x in arr if x > pivot]\n    return quick_sort(left) + middle + quick_sort(right)" },
                    new CodeBlock { Language = "C#", Code = "public void QuickSort(int[] arr)\n{\n    QuickSort(arr, 0, arr.Length - 1);\n}\n\nprivate void QuickSort(int[] arr, int low, int high)\n{\n    if (low < high)\n    {\n        int pivotIndex = Partition(arr, low, high);\n        QuickSort(arr, low, pivotIndex - 1);\n        QuickSort(arr, pivotIndex + 1, high);\n    }\n}\n\nprivate int Partition(int[] arr, int low, int high)\n{\n    int pivot = arr[high];\n    int i = low - 1;\n    for (int j = low; j < high; j++)\n    {\n        if (arr[j] < pivot)\n        {\n            i++;\n            int temp = arr[i];\n            arr[i] = arr[j];\n            arr[j] = temp;\n        }\n    }\n    int temp1 = arr[i + 1];\n    arr[i + 1] = arr[high];\n    arr[high] = temp1;\n    return i + 1;\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function quickSort(arr) {\n    if (arr.length <= 1) return arr;\n    const pivot = arr[Math.floor(arr.length / 2)];\n    const left = arr.filter(x => x < pivot);\n    const middle = arr.filter(x => x === pivot);\n    const right = arr.filter(x => x > pivot);\n    return quickSort(left).concat(middle, quickSort(right));\n}" }
                ],
                Category = sortingCategory
            },
            new ()
            {
                Name = "Heap",
                Description = "Heap sort leverages a binary heap data structure to efficiently sort elements.",
                Complexity = Complexity.Linearithmic,
                UseCases = ["Efficient sorting", "Large datasets"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def heap_sort(arr):\n    n = len(arr)\n    for i in range(n // 2 - 1, -1, -1):\n        heapify(arr, n, i)\n    for i in range(n - 1, 0, -1):\n        arr[0], arr[i] = arr[i], arr[0]\n        heapify(arr, i, 0)\n    return arr\n\ndef heapify(arr, n, i):\n    largest = i\n    l = 2 * i + 1\n    r = 2 * i + 2\n    if l < n and arr[l] > arr[largest]:\n        largest = l\n    if r < n and arr[r] > arr[largest]:\n        largest = r\n    if largest != i:\n        arr[i], arr[largest] = arr[largest], arr[i]\n        heapify(arr, n, largest)" },
                    new CodeBlock { Language = "C#", Code = "public void HeapSort(int[] arr)\n{\n    int n = arr.Length;\n    for (int i = n / 2 - 1; i >= 0; i--)\n    {\n        Heapify(arr, n, i);\n    }\n    for (int i = n - 1; i > 0; i--)\n    {\n        int temp = arr[0];\n        arr[0] = arr[i];\n        arr[i] = temp;\n        Heapify(arr, i, 0);\n    }\n}\n\nprivate void Heapify(int[] arr, int n, int i)\n{\n    int largest = i;\n    int left = 2 * i + 1;\n    int right = 2 * i + 2;\n    if (left < n && arr[left] > arr[largest])\n    {\n        largest = left;\n    }\n    if (right < n && arr[right] > arr[largest])\n    {\n        largest = right;\n    }\n    if (largest != i)\n    {\n        int swap = arr[i];\n        arr[i] = arr[largest];\n        arr[largest] = swap;\n        Heapify(arr, n, largest);\n    }\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function heapSort(arr) {\n    let n = arr.length;\n    for (let i = Math.floor(n / 2) - 1; i >= 0; i--) {\n        heapify(arr, n, i);\n    }\n    for (let i = n - 1; i > 0; i--) {\n        [arr[0], arr[i]] = [arr[i], arr[0]];\n        heapify(arr, i, 0);\n    }\n    return arr;\n}\n\nfunction heapify(arr, n, i) {\n    let largest = i;\n    let left = 2 * i + 1;\n    let right = 2 * i + 2;\n    if (left < n && arr[left] > arr[largest]) {\n        largest = left;\n    }\n    if (right < n && arr[right] > arr[largest]) {\n        largest = right;\n    }\n    if (largest != i) {\n        [arr[i], arr[largest]] = [arr[largest], arr[i]];\n        heapify(arr, n, largest);\n    }\n}" }
                ],
                Category = sortingCategory
            },
            new ()
            {
                Name = "Merge",
                Description = "Merge sort divides the array into halves, sorts them, and then merges the sorted halves.",
                Complexity = Complexity.Linearithmic,
                UseCases = ["Stable sorting", "Large datasets"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def merge_sort(arr):\n    if len(arr) <= 1:\n        return arr\n    mid = len(arr) // 2\n    left = merge_sort(arr[:mid])\n    right = merge_sort(arr[mid:])\n    return merge(left, right)\n\ndef merge(left, right):\n    result = []\n    i = j = 0\n    while i < len(left) and j < len(right):\n        if left[i] < right[j]:\n            result.append(left[i])\n            i += 1\n        else:\n            result.append(right[j])\n            j += 1\n    result.extend(left[i:])\n    result.extend(right[j:])\n    return result" },
                    new CodeBlock { Language = "C#", Code = "public void MergeSort(int[] arr)\n{\n    MergeSort(arr, 0, arr.Length - 1);\n}\n\nprivate void MergeSort(int[] arr, int left, int right)\n{\n    if (left < right)\n    {\n        int mid = (left + right) / 2;\n        MergeSort(arr, left, mid);\n        MergeSort(arr, mid + 1, right);\n        Merge(arr, left, mid, right);\n    }\n}\n\nprivate void Merge(int[] arr, int left, int mid, int right)\n{\n    int n1 = mid - left + 1;\n    int n2 = right - mid;\n    int[] L = new int[n1];\n    int[] R = new int[n2];\n    for (int i = 0; i < n1; i++)\n        L[i] = arr[left + i];\n    for (int j = 0; j < n2; j++)\n        R[j] = arr[mid + 1 + j];\n    int i_index = 0, j_index = 0, k = left;\n    while (i_index < n1 && j_index < n2)\n    {\n        if (L[i_index] <= R[j_index])\n        {\n            arr[k] = L[i_index];\n            i_index++;\n        }\n        else\n        {\n            arr[k] = R[j_index];\n            j_index++;\n        }\n        k++;\n    }\n    while (i_index < n1)\n    {\n        arr[k] = L[i_index];\n        i_index++;\n        k++;\n    }\n    while (j_index < n2)\n    {\n        arr[k] = R[j_index];\n        j_index++;\n        k++;\n    }\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function mergeSort(arr) {\n    if (arr.length <= 1) return arr;\n    const mid = Math.floor(arr.length / 2);\n    const left = mergeSort(arr.slice(0, mid));\n    const right = mergeSort(arr.slice(mid));\n    return merge(left, right);\n}\n\nfunction merge(left, right) {\n    let result = [];\n    let i = 0, j = 0;\n    while (i < left.length && j < right.length) {\n        if (left[i] < right[j]) {\n            result.push(left[i]);\n            i++;\n        } else {\n            result.push(right[j]);\n            j++;\n        }\n    }\n    return result.concat(left.slice(i)).concat(right.slice(j));\n}" }
                ],
                Category = sortingCategory
            },
            new ()
            {
                Name = "Radix",
                Description = "Radix sort processes integers by sorting individual digits into buckets.",
                Complexity = Complexity.Linear,
                UseCases = ["Sorting numbers", "Datasets with a known range"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def radix_sort(arr):\n    if len(arr) == 0:\n        return arr\n    max_val = max(arr)\n    exp = 1\n    while max_val // exp > 0:\n        arr = counting_sort(arr, exp)\n        exp *= 10\n    return arr\n\ndef counting_sort(arr, exp):\n    n = len(arr)\n    output = [0] * n\n    count = [0] * 10\n    for i in range(n):\n        index = (arr[i] // exp) % 10\n        count[index] += 1\n    for i in range(1, 10):\n        count[i] += count[i - 1]\n    i = n - 1\n    while i >= 0:\n        index = (arr[i] // exp) % 10\n        output[count[index] - 1] = arr[i]\n        count[index] -= 1\n        i -= 1\n    return output" },
                    new CodeBlock { Language = "C#", Code = "public void RadixSort(int[] arr)\n{\n    if (arr.Length == 0) return;\n    int max = arr[0];\n    for (int i = 1; i < arr.Length; i++)\n    {\n        if (arr[i] > max) max = arr[i];\n    }\n    for (int exp = 1; max / exp > 0; exp *= 10)\n    {\n        CountingSort(arr, exp);\n    }\n}\n\nprivate void CountingSort(int[] arr, int exp)\n{\n    int n = arr.Length;\n    int[] output = new int[n];\n    int[] count = new int[10];\n    for (int i = 0; i < n; i++)\n    {\n        count[(arr[i] / exp) % 10]++;\n    }\n    for (int i = 1; i < 10; i++)\n    {\n        count[i] += count[i - 1];\n    }\n    for (int i = n - 1; i >= 0; i--)\n    {\n        int index = (arr[i] / exp) % 10;\n        output[count[index] - 1] = arr[i];\n        count[index]--;\n    }\n    for (int i = 0; i < n; i++)\n    {\n        arr[i] = output[i];\n    }\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function radixSort(arr) {\n    if (arr.length === 0) return arr;\n    let max = Math.max(...arr);\n    for (let exp = 1; Math.floor(max / exp) > 0; exp *= 10) {\n        arr = countingSort(arr, exp);\n    }\n    return arr;\n}\n\nfunction countingSort(arr, exp) {\n    let n = arr.length;\n    let output = new Array(n).fill(0);\n    let count = new Array(10).fill(0);\n    for (let i = 0; i < n; i++) {\n        count[Math.floor(arr[i] / exp) % 10]++;\n    }\n    for (let i = 1; i < 10; i++) {\n        count[i] += count[i - 1];\n    }\n    for (let i = n - 1; i >= 0; i--) {\n        let index = Math.floor(arr[i] / exp) % 10;\n        output[count[index] - 1] = arr[i];\n        count[index]--;\n    }\n    return output;\n}" }
                ],
                Category = sortingCategory
            },
            new ()
            {
                Name = "Counting",
                Description = "Counting sort counts the occurrences of each distinct element and calculates positions in the sorted array.",
                Complexity = Complexity.Linear,
                UseCases = ["Sorting integers", "Datasets with limited range"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def counting_sort(arr):\n    if not arr:\n        return arr\n    max_val = max(arr)\n    min_val = min(arr)\n    range_of_elements = max_val - min_val + 1\n    count = [0] * range_of_elements\n    output = [0] * len(arr)\n    for number in arr:\n        count[number - min_val] += 1\n    for i in range(1, len(count)):\n        count[i] += count[i - 1]\n    for number in reversed(arr):\n        output[count[number - min_val] - 1] = number\n        count[number - min_val] -= 1\n    return output" },
                    new CodeBlock { Language = "C#", Code = "public void CountingSort(int[] arr)\n{\n    if (arr == null || arr.Length == 0) return;\n    int max = arr[0], min = arr[0];\n    for (int i = 1; i < arr.Length; i++)\n    {\n        if (arr[i] > max) max = arr[i];\n        if (arr[i] < min) min = arr[i];\n    }\n    int range = max - min + 1;\n    int[] count = new int[range];\n    int[] output = new int[arr.Length];\n    for (int i = 0; i < arr.Length; i++)\n    {\n        count[arr[i] - min]++;\n    }\n    for (int i = 1; i < count.Length; i++)\n    {\n        count[i] += count[i - 1];\n    }\n    for (int i = arr.Length - 1; i >= 0; i--)\n    {\n        output[count[arr[i] - min] - 1] = arr[i];\n        count[arr[i] - min]--;\n    }\n    for (int i = 0; i < arr.Length; i++)\n    {\n        arr[i] = output[i];\n    }\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function countingSort(arr) {\n    if (arr.length === 0) return arr;\n    let max = Math.max(...arr);\n    let min = Math.min(...arr);\n    let range = max - min + 1;\n    let count = new Array(range).fill(0);\n    let output = new Array(arr.length).fill(0);\n    for (let i = 0; i < arr.length; i++) {\n        count[arr[i] - min]++;\n    }\n    for (let i = 1; i < count.length; i++) {\n        count[i] += count[i - 1];\n    }\n    for (let i = arr.length - 1; i >= 0; i--) {\n        output[count[arr[i] - min] - 1] = arr[i];\n        count[arr[i] - min]--;\n    }\n    return output;\n}" }
                ],
                Category = sortingCategory
            }
        };

        await aRepository.AddRangeAsync(sortingAlgorithms);

        #endregion

        #region Searching

        var searchCategory = new AlgorithmCategory
        {
            Name = "Search",
            Description = "Algorithms for searching and traversing data structures."
        };

        var searchAlgorithms = new Algorithm[]
        {
            new ()
            {
                Name = "DFS",
                Description = "Depth-first search traverses as far as possible along each branch before backtracking.",
                Complexity = Complexity.GraphTraversal,
                UseCases = ["Graph traversal", "Pathfinding"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def dfs(graph, start, visited=None):\n    if visited is None:\n        visited = set()\n    visited.add(start)\n    for neighbor in graph[start]:\n        if neighbor not in visited:\n            dfs(graph, neighbor, visited)\n    return visited" },
                    new CodeBlock { Language = "C#", Code = "public void DFS(Node start)\n{\n    if (start == null) return;\n    start.Visited = true;\n    foreach (var neighbor in start.Neighbors)\n    {\n        if (!neighbor.Visited)\n        {\n            DFS(neighbor);\n        }\n    }\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function dfs(node, visited = new Set()) {\n    visited.add(node);\n    for (const neighbor of node.neighbors) {\n        if (!visited.has(neighbor)) {\n            dfs(neighbor, visited);\n        }\n    }\n    return visited;\n}" }
                ],
                Category = searchCategory
            },
            new ()
            {
                Name = "BFS",
                Description = "Breadth-first search explores neighbor nodes level by level.",
                Complexity = Complexity.GraphTraversal,
                UseCases = ["Graph traversal", "Shortest path in unweighted graphs"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def bfs(graph, start):\n    visited = set()\n    queue = [start]\n    while queue:\n        vertex = queue.pop(0)\n        if vertex not in visited:\n            visited.add(vertex)\n            queue.extend(n for n in graph[vertex] if n not in visited)\n    return visited" },
                    new CodeBlock { Language = "C#", Code = "public void BFS(Node start) {\n    var visited = new HashSet<Node>();\n    var queue = new Queue<Node>();\n    queue.Enqueue(start);\n    while (queue.Count > 0) {\n        var node = queue.Dequeue();\n        if (!visited.Contains(node)) {\n            visited.Add(node);\n            foreach (var neighbor in node.Neighbors) {\n                if (!visited.Contains(neighbor))\n                    queue.Enqueue(neighbor);\n            }\n        }\n    }\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function bfs(root) {\n    const visited = new Set();\n    const queue = [root];\n    while (queue.length > 0) {\n        const node = queue.shift();\n        if (!visited.has(node)) {\n            visited.add(node);\n            for (const neighbor of node.neighbors) {\n                if (!visited.has(neighbor))\n                    queue.push(neighbor);\n            }\n        }\n    }\n}" }
                ],
                Category = searchCategory
            },
            new ()
            {
                Name = "Binary",
                Description = "Binary search efficiently finds an element in a sorted array by repeatedly dividing the search interval in half.",
                Complexity = Complexity.Logarithmic,
                UseCases = ["Searching sorted arrays", "Efficient lookups"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def binary_search(arr, target):\n    left = 0\n    right = len(arr) - 1\n    while left <= right:\n        mid = (left + right) // 2\n        if arr[mid] == target:\n            return mid\n        elif arr[mid] < target:\n            left = mid + 1\n        else:\n            right = mid - 1\n    return -1" },
                    new CodeBlock { Language = "C#", Code = "public int BinarySearch(int[] arr, int target)\n{\n    int left = 0;\n    int right = arr.Length - 1;\n    while (left <= right)\n    {\n        int mid = left + (right - left) / 2;\n        if (arr[mid] == target)\n            return mid;\n        else if (arr[mid] < target)\n            left = mid + 1;\n        else\n            right = mid - 1;\n    }\n    return -1;\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function binarySearch(arr, target) {\n    let left = 0;\n    let right = arr.length - 1;\n    while (left <= right) {\n        let mid = Math.floor((left + right) / 2);\n        if (arr[mid] === target)\n            return mid;\n        else if (arr[mid] < target)\n            left = mid + 1;\n        else\n            right = mid - 1;\n    }\n    return -1;\n}" }
                ],
                Category = searchCategory
            },
            new ()
            {
                Name = "Linear",
                Description = "Linear search checks each element sequentially until the target is found.",
                Complexity = Complexity.Linear,
                UseCases = ["Unsorted arrays", "Small datasets"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def linear_search(arr, target):\n    for i, value in enumerate(arr):\n        if value == target:\n            return i\n    return -1" },
                    new CodeBlock { Language = "C#", Code = "public int LinearSearch(int[] arr, int target)\n{\n    for (int i = 0; i < arr.Length; i++)\n    {\n        if (arr[i] == target)\n            return i;\n    }\n    return -1;\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function linearSearch(arr, target) {\n    for (let i = 0; i < arr.length; i++) {\n        if (arr[i] === target)\n            return i;\n    }\n    return -1;\n}" }
                ],
                Category = searchCategory
            }
        };

        await aRepository.AddRangeAsync(searchAlgorithms);

        #endregion

        #region Graph

        var graphCategory = new AlgorithmCategory
        {
            Name = "Graph",
            Description = "Algorithms that operate on graph data structures, solving problems like shortest paths and minimum spanning trees."
        };

        var graphAlgorithms = new Algorithm[]
        {
            new ()
            {
                Name = "Dijkstra",
                Description = "Dijkstra's algorithm finds the shortest path from a single source to all other vertices in a graph with non-negative edge weights.",
                Complexity = Complexity.Linearithmic,
                UseCases = ["Shortest path", "Routing"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def dijkstra(graph, start):\n    import heapq\n    distances = {vertex: float('inf') for vertex in graph}\n    distances[start] = 0\n    pq = [(0, start)]\n    while pq:\n        current_distance, current_vertex = heapq.heappop(pq)\n        if current_distance > distances[current_vertex]:\n            continue\n        for neighbor, weight in graph[current_vertex]:\n            distance = current_distance + weight\n            if distance < distances[neighbor]:\n                distances[neighbor] = distance\n                heapq.heappush(pq, (distance, neighbor))\n    return distances" },
                    new CodeBlock { Language = "C#", Code = "public Dictionary<Node, int> Dijkstra(Graph graph, Node start)\n{\n    var distances = new Dictionary<Node, int>();\n    foreach (var node in graph.Nodes)\n    {\n        distances[node] = int.MaxValue;\n    }\n    distances[start] = 0;\n    var queue = new PriorityQueue<Node, int>();\n    queue.Enqueue(start, 0);\n    while (queue.Count > 0)\n    {\n        var current = queue.Dequeue();\n        int currentDistance = distances[current];\n        foreach (var edge in graph.GetNeighbors(current))\n        {\n            int newDistance = currentDistance + edge.Weight;\n            if (newDistance < distances[edge.Target])\n            {\n                distances[edge.Target] = newDistance;\n                queue.Enqueue(edge.Target, newDistance);\n            }\n        }\n    }\n    return distances;\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function dijkstra(graph, start) {\n    const distances = {};\n    for (const vertex in graph) {\n        distances[vertex] = Infinity;\n    }\n    distances[start] = 0;\n    const queue = [start];\n    while (queue.length > 0) {\n        queue.sort((a, b) => distances[a] - distances[b]);\n        const current = queue.shift();\n        for (const [neighbor, weight] of graph[current]) {\n            const newDistance = distances[current] + weight;\n            if (newDistance < distances[neighbor]) {\n                distances[neighbor] = newDistance;\n                queue.push(neighbor);\n            }\n        }\n    }\n    return distances;\n}" }
                ],
                Category = graphCategory
            },
            new() {
                Name = "Bellman-Ford",
                Description = "Bellman-Ford algorithm computes shortest paths from a single source in graphs with negative weights, and can detect negative cycles.",
                Complexity = Complexity.Quadratic,
                UseCases = ["Shortest path", "Negative weight edges", "Cycle detection"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def bellman_ford(graph, start):\n    distances = {vertex: float('inf') for vertex in graph}\n    distances[start] = 0\n    for _ in range(len(graph) - 1):\n        for vertex in graph:\n            for neighbor, weight in graph[vertex]:\n                if distances[vertex] + weight < distances[neighbor]:\n                    distances[neighbor] = distances[vertex] + weight\n    for vertex in graph:\n        for neighbor, weight in graph[vertex]:\n            if distances[vertex] + weight < distances[neighbor]:\n                return None\n    return distances" },
                    new CodeBlock { Language = "C#", Code = "public Dictionary<Node, int> BellmanFord(Graph graph, Node start)\n{\n    var distances = new Dictionary<Node, int>();\n    foreach (var node in graph.Nodes)\n    {\n        distances[node] = int.MaxValue;\n    }\n    distances[start] = 0;\n    int count = graph.Nodes.Count;\n    for (int i = 0; i < count - 1; i++)\n    {\n        foreach (var node in graph.Nodes)\n        {\n            foreach (var edge in graph.GetEdges(node))\n            {\n                if (distances[node] != int.MaxValue && distances[node] + edge.Weight < distances[edge.Target])\n                {\n                    distances[edge.Target] = distances[node] + edge.Weight;\n                }\n            }\n        }\n    }\n    foreach (var node in graph.Nodes)\n    {\n        foreach (var edge in graph.GetEdges(node))\n        {\n            if (distances[node] != int.MaxValue && distances[node] + edge.Weight < distances[edge.Target])\n            {\n                return null;\n            }\n        }\n    }\n    return distances;\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function bellmanFord(graph, start) {\n    const distances = {};\n    for (const vertex in graph) {\n        distances[vertex] = Infinity;\n    }\n    distances[start] = 0;\n    const vertices = Object.keys(graph);\n    for (let i = 0; i < vertices.length - 1; i++) {\n        for (const vertex of vertices) {\n            for (const edge of graph[vertex]) {\n                const [neighbor, weight] = edge;\n                if (distances[vertex] + weight < distances[neighbor]) {\n                    distances[neighbor] = distances[vertex] + weight;\n                }\n            }\n        }\n    }\n    for (const vertex of vertices) {\n        for (const edge of graph[vertex]) {\n            const [neighbor, weight] = edge;\n            if (distances[vertex] + weight < distances[neighbor]) {\n                return null;\n            }\n        }\n    }\n    return distances;\n}" }
                ],
                Category = graphCategory
            },
            new ()
            {
                Name = "Floyd-Warshall",
                Description = "Floyd-Warshall algorithm finds the shortest paths between all pairs of vertices in a weighted graph.",
                Complexity = Complexity.Cubic,
                UseCases = ["All-pairs shortest path", "Dense graphs"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def bellman_ford(graph, start):\n    distances = {vertex: float('inf') for vertex in graph}\n    distances[start] = 0\n    for _ in range(len(graph) - 1):\n        for vertex in graph:\n            for neighbor, weight in graph[vertex]:\n                if distances[vertex] + weight < distances[neighbor]:\n                    distances[neighbor] = distances[vertex] + weight\n    for vertex in graph:\n        for neighbor, weight in graph[vertex]:\n            if distances[vertex] + weight < distances[neighbor]:\n                return None\n    return distances" },
                    new CodeBlock { Language = "C#", Code = "public Dictionary<Node, int> BellmanFord(Graph graph, Node start)\n{\n    var distances = new Dictionary<Node, int>();\n    foreach (var node in graph.Nodes)\n    {\n        distances[node] = int.MaxValue;\n    }\n    distances[start] = 0;\n    int count = graph.Nodes.Count;\n    for (int i = 0; i < count - 1; i++)\n    {\n        foreach (var node in graph.Nodes)\n        {\n            foreach (var edge in graph.GetEdges(node))\n            {\n                if (distances[node] != int.MaxValue && distances[node] + edge.Weight < distances[edge.Target])\n                {\n                    distances[edge.Target] = distances[node] + edge.Weight;\n                }\n            }\n        }\n    }\n    foreach (var node in graph.Nodes)\n    {\n        foreach (var edge in graph.GetEdges(node))\n        {\n            if (distances[node] != int.MaxValue && distances[node] + edge.Weight < distances[edge.Target])\n            {\n                return null;\n            }\n        }\n    }\n    return distances;\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function bellmanFord(graph, start) {\n    const distances = {};\n    for (const vertex in graph) {\n        distances[vertex] = Infinity;\n    }\n    distances[start] = 0;\n    const vertices = Object.keys(graph);\n    for (let i = 0; i < vertices.length - 1; i++) {\n        for (const vertex of vertices) {\n            for (const edge of graph[vertex]) {\n                const [neighbor, weight] = edge;\n                if (distances[vertex] + weight < distances[neighbor]) {\n                    distances[neighbor] = distances[vertex] + weight;\n                }\n            }\n        }\n    }\n    for (const vertex of vertices) {\n        for (const edge of graph[vertex]) {\n            const [neighbor, weight] = edge;\n            if (distances[vertex] + weight < distances[neighbor]) {\n                return null;\n            }\n        }\n    }\n    return distances;\n}" }
                ],
                Category = graphCategory
            },
            new ()
            {
                Name = "Kruskal",
                Description = "Kruskal's algorithm finds the minimum spanning tree by sorting edges and using union-find for cycle detection.",
                Complexity = Complexity.Linearithmic,
                UseCases = ["Minimum spanning tree", "Network design"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def kruskal(graph):\n    parent = {}\n    rank = {}\n    def find(x):\n        if parent[x] != x:\n            parent[x] = find(parent[x])\n        return parent[x]\n    def union(x, y):\n        rootX = find(x)\n        rootY = find(y)\n        if rootX != rootY:\n            if rank[rootX] < rank[rootY]:\n                parent[rootX] = rootY\n            elif rank[rootX] > rank[rootY]:\n                parent[rootY] = rootX\n            else:\n                parent[rootY] = rootX\n                rank[rootX] += 1\n    vertices = list(graph.keys())\n    for vertex in vertices:\n        parent[vertex] = vertex\n        rank[vertex] = 0\n    edges = []\n    for u in graph:\n        for v, w in graph[u]:\n            if u < v:\n                edges.append((u, v, w))\n    edges.sort(key=lambda x: x[2])\n    mst = []\n    for u, v, w in edges:\n        if find(u) != find(v):\n            union(u, v)\n            mst.append((u, v, w))\n    return mst" },
                    new CodeBlock { Language = "C#", Code = "public List<Edge> Kruskal(Graph graph)\n{\n    var parent = new Dictionary<Node, Node>();\n    var rank = new Dictionary<Node, int>();\n    foreach (var node in graph.Nodes)\n    {\n        parent[node] = node;\n        rank[node] = 0;\n    }\n    Node Find(Node n)\n    {\n        if (!parent[n].Equals(n))\n            parent[n] = Find(parent[n]);\n        return parent[n];\n    }\n    void Union(Node x, Node y)\n    {\n        var rootX = Find(x);\n        var rootY = Find(y);\n        if (rootX.Equals(rootY))\n            return;\n        if (rank[rootX] < rank[rootY])\n            parent[rootX] = rootY;\n        else if (rank[rootX] > rank[rootY])\n            parent[rootY] = rootX;\n        else\n        {\n            parent[rootY] = rootX;\n            rank[rootX]++;\n        }\n    }\n    var edges = new List<Edge>();\n    foreach (var node in graph.Nodes)\n    {\n        foreach (var edge in graph.GetEdges(node))\n        {\n            if (edge.Source.CompareTo(edge.Target) < 0)\n                edges.Add(edge);\n        }\n    }\n    edges.Sort((a, b) => a.Weight.CompareTo(b.Weight));\n    var mst = new List<Edge>();\n    foreach (var edge in edges)\n    {\n        if (!Find(edge.Source).Equals(Find(edge.Target)))\n        {\n            Union(edge.Source, edge.Target);\n            mst.Add(edge);\n        }\n    }\n    return mst;\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function kruskal(graph) {\n    const parent = {};\n    const rank = {};\n    for (const vertex in graph) {\n        parent[vertex] = vertex;\n        rank[vertex] = 0;\n    }\n    function find(x) {\n        if (parent[x] !== x) {\n            parent[x] = find(parent[x]);\n        }\n        return parent[x];\n    }\n    function union(x, y) {\n        const rootX = find(x);\n        const rootY = find(y);\n        if (rootX === rootY) return;\n        if (rank[rootX] < rank[rootY]) {\n            parent[rootX] = rootY;\n        } else if (rank[rootX] > rank[rootY]) {\n            parent[rootY] = rootX;\n        } else {\n            parent[rootY] = rootX;\n            rank[rootX]++;\n        }\n    }\n    const edges = [];\n    for (const u in graph) {\n        for (const edge of graph[u]) {\n            const v = edge[0];\n            const w = edge[1];\n            if (u < v) {\n                edges.push([u, v, w]);\n            }\n        }\n    }\n    edges.sort((a, b) => a[2] - b[2]);\n    const mst = [];\n    for (const [u, v, w] of edges) {\n        if (find(u) !== find(v)) {\n            union(u, v);\n            mst.push([u, v, w]);\n        }\n    }\n    return mst;\n}" }
                ],
                Category = graphCategory
            },
            new ()
            {
                Name = "Prim",
                Description = "Prim's algorithm builds the minimum spanning tree by expanding the tree one edge at a time.",
                Complexity = Complexity.Linearithmic,
                UseCases = ["Minimum spanning tree", "Network design"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def prim(graph):\n    import heapq\n    start = next(iter(graph))\n    visited = set()\n    mst = []\n    heap = []\n    for neighbor, weight in graph[start]:\n        heapq.heappush(heap, (weight, start, neighbor))\n    visited.add(start)\n    while heap:\n        weight, u, v = heapq.heappop(heap)\n        if v not in visited:\n            visited.add(v)\n            mst.append((u, v, weight))\n            for neighbor, w in graph[v]:\n                if neighbor not in visited:\n                    heapq.heappush(heap, (w, v, neighbor))\n    return mst" },
                    new CodeBlock { Language = "C#", Code = "public List<Edge> Prim(Graph graph)\n{\n    var mst = new List<Edge>();\n    var visited = new HashSet<Node>();\n    var start = graph.Nodes[0];\n    visited.Add(start);\n    var queue = new PriorityQueue<Edge, int>();\n    foreach (var edge in graph.GetEdges(start))\n    {\n        queue.Enqueue(edge, edge.Weight);\n    }\n    while (queue.Count > 0)\n    {\n        var edge = queue.Dequeue();\n        if (!visited.Contains(edge.Target))\n        {\n            visited.Add(edge.Target);\n            mst.Add(edge);\n            foreach (var next in graph.GetEdges(edge.Target))\n            {\n                if (!visited.Contains(next.Target))\n                    queue.Enqueue(next, next.Weight);\n            }\n        }\n    }\n    return mst;\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function prim(graph) {\n    const vertices = Object.keys(graph);\n    const start = vertices[0];\n    const visited = new Set();\n    visited.add(start);\n    const mst = [];\n    const heap = [];\n    function pushHeap(edge) {\n        heap.push(edge);\n        heap.sort((a, b) => a.weight - b.weight);\n    }\n    for (const [neighbor, weight] of graph[start]) {\n        pushHeap({ u: start, v: neighbor, weight: weight });\n    }\n    while (heap.length > 0) {\n        const edge = heap.shift();\n        if (!visited.has(edge.v)) {\n            visited.add(edge.v);\n            mst.push(edge);\n            for (const [nextNeighbor, w] of graph[edge.v]) {\n                if (!visited.has(nextNeighbor)) {\n                    pushHeap({ u: edge.v, v: nextNeighbor, weight: w });\n                }\n            }\n        }\n    }\n    return mst;\n}" }
                ],
                Category = graphCategory
            },
            new ()
            {
                Name = "A*",
                Description = "A* search algorithm finds the shortest path by combining heuristics with path cost.",
                Complexity = Complexity.Linearithmic,
                UseCases = ["Pathfinding", "Game AI", "Navigation"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def a_star(graph, start, goal):\n    import heapq\n    open_set = []\n    heapq.heappush(open_set, (0, start))\n    came_from = {}\n    g_score = {vertex: float('inf') for vertex in graph}\n    g_score[start] = 0\n    f_score = {vertex: float('inf') for vertex in graph}\n    f_score[start] = heuristic(start, goal)\n    while open_set:\n        current = heapq.heappop(open_set)[1]\n        if current == goal:\n            return reconstruct_path(came_from, current)\n        for neighbor, weight in graph[current]:\n            tentative = g_score[current] + weight\n            if tentative < g_score[neighbor]:\n                came_from[neighbor] = current\n                g_score[neighbor] = tentative\n                f_score[neighbor] = tentative + heuristic(neighbor, goal)\n                if neighbor not in [n for _, n in open_set]:\n                    heapq.heappush(open_set, (f_score[neighbor], neighbor))\n    return None\n\ndef reconstruct_path(came_from, current):\n    path = [current]\n    while current in came_from:\n        current = came_from[current]\n        path.append(current)\n    return path[::-1]\n\ndef heuristic(a, b):\n    return 0" },
                    new CodeBlock { Language = "C#", Code = "public List<Node> AStar(Graph graph, Node start, Node goal)\n{\n    var openSet = new PriorityQueue<Node, double>();\n    openSet.Enqueue(start, 0);\n    var cameFrom = new Dictionary<Node, Node>();\n    var gScore = new Dictionary<Node, double>();\n    foreach (var node in graph.Nodes)\n    {\n        gScore[node] = double.PositiveInfinity;\n    }\n    gScore[start] = 0;\n    var fScore = new Dictionary<Node, double>();\n    foreach (var node in graph.Nodes)\n    {\n        fScore[node] = double.PositiveInfinity;\n    }\n    fScore[start] = Heuristic(start, goal);\n    while (openSet.Count > 0)\n    {\n        var current = openSet.Dequeue();\n        if (current.Equals(goal))\n        {\n            return ReconstructPath(cameFrom, current);\n        }\n        foreach (var edge in graph.GetEdges(current))\n        {\n            var tentative = gScore[current] + edge.Weight;\n            if (tentative < gScore[edge.Target])\n            {\n                cameFrom[edge.Target] = current;\n                gScore[edge.Target] = tentative;\n                fScore[edge.Target] = tentative + Heuristic(edge.Target, goal);\n                if (!openSet.UnorderedItems.Any(item => item.Element.Equals(edge.Target)))\n                {\n                    openSet.Enqueue(edge.Target, fScore[edge.Target]);\n                }\n            }\n        }\n    }\n    return null;\n}\n\npublic List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)\n{\n    var totalPath = new List<Node> { current };\n    while (cameFrom.ContainsKey(current))\n    {\n        current = cameFrom[current];\n        totalPath.Insert(0, current);\n    }\n    return totalPath;\n}\n\npublic double Heuristic(Node a, Node b)\n{\n    return 0;\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function aStar(graph, start, goal) {\n    const openSet = [];\n    openSet.push(start);\n    const cameFrom = {};\n    const gScore = {};\n    for (const vertex in graph) {\n        gScore[vertex] = Infinity;\n    }\n    gScore[start] = 0;\n    const fScore = {};\n    for (const vertex in graph) {\n        fScore[vertex] = Infinity;\n    }\n    fScore[start] = heuristic(start, goal);\n    while (openSet.length > 0) {\n        openSet.sort((a, b) => fScore[a] - fScore[b]);\n        const current = openSet.shift();\n        if (current === goal) {\n            return reconstructPath(cameFrom, current);\n        }\n        for (const [neighbor, weight] of graph[current]) {\n            const tentative = gScore[current] + weight;\n            if (tentative < gScore[neighbor]) {\n                cameFrom[neighbor] = current;\n                gScore[neighbor] = tentative;\n                fScore[neighbor] = tentative + heuristic(neighbor, goal);\n                if (!openSet.includes(neighbor)) {\n                    openSet.push(neighbor);\n                }\n            }\n        }\n    }\n    return null;\n}\n\nfunction reconstructPath(cameFrom, current) {\n    const totalPath = [current];\n    while (current in cameFrom) {\n        current = cameFrom[current];\n        totalPath.unshift(current);\n    }\n    return totalPath;\n}\n\nfunction heuristic(a, b) {\n    return 0;\n}" }
                ],
                Category = graphCategory
            }
        };

        await aRepository.AddRangeAsync(graphAlgorithms);

        #endregion

        #region Dynamic Programming

        var dynamicProgrammingCategory = new AlgorithmCategory
        {
            Name = "Dynamic Programming",
            Description = "Algorithms that solve complex problems by breaking them down into overlapping subproblems and caching results."
        };

        var dynamicProgrammingAlgorithms = new Algorithm[]
        {
            new ()
            {
                Name = "Knapsack",
                Description = "Solves the knapsack problem by finding the optimal subset of items to maximize value within a weight constraint.",
                Complexity = Complexity.PseudoPolynomial,
                UseCases = ["Resource allocation", "Optimization"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def knapsack(weights, values, capacity):\n    n = len(weights)\n    dp = [[0] * (capacity + 1) for _ in range(n + 1)]\n    for i in range(1, n + 1):\n        for w in range(1, capacity + 1):\n            if weights[i - 1] <= w:\n                dp[i][w] = max(dp[i - 1][w], dp[i - 1][w - weights[i - 1]] + values[i - 1])\n            else:\n                dp[i][w] = dp[i - 1][w]\n    return dp[n][capacity]" },
                    new CodeBlock { Language = "C#", Code = "public int Knapsack(int[] weights, int[] values, int capacity)\n{\n    int n = weights.Length;\n    int[,] dp = new int[n + 1, capacity + 1];\n    for (int i = 1; i <= n; i++)\n    {\n        for (int w = 1; w <= capacity; w++)\n        {\n            if (weights[i - 1] <= w)\n                dp[i, w] = Math.Max(dp[i - 1, w], dp[i - 1, w - weights[i - 1]] + values[i - 1]);\n            else\n                dp[i, w] = dp[i - 1, w];\n        }\n    }\n    return dp[n, capacity];\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function knapsack(weights, values, capacity) {\n    const n = weights.length;\n    const dp = Array.from({ length: n + 1 }, () => Array(capacity + 1).fill(0));\n    for (let i = 1; i <= n; i++) {\n        for (let w = 1; w <= capacity; w++) {\n            if (weights[i - 1] <= w)\n                dp[i][w] = Math.max(dp[i - 1][w], dp[i - 1][w - weights[i - 1]] + values[i - 1]);\n            else\n                dp[i][w] = dp[i - 1][w];\n        }\n    }\n    return dp[n][capacity];\n}" }
                ],
                Category = dynamicProgrammingCategory
            },
            new ()
            {
                Name = "LCS",
                Description = "Finds the longest common subsequence (LCS) between two sequences.",
                Complexity = Complexity.Quadratic,
                UseCases = ["Text comparison", "Diff tools"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def lcs(X, Y):\n    m = len(X)\n    n = len(Y)\n    dp = [[0]*(n+1) for _ in range(m+1)]\n    for i in range(m):\n        for j in range(n):\n            if X[i] == Y[j]:\n                dp[i+1][j+1] = dp[i][j] + 1\n            else:\n                dp[i+1][j+1] = max(dp[i+1][j], dp[i][j+1])\n    i, j = m, n\n    lcs_str = []\n    while i > 0 and j > 0:\n        if X[i-1] == Y[j-1]:\n            lcs_str.append(X[i-1])\n            i -= 1\n            j -= 1\n        elif dp[i-1][j] >= dp[i][j-1]:\n            i -= 1\n        else:\n            j -= 1\n    return ''.join(reversed(lcs_str))" },
                    new CodeBlock { Language = "C#", Code = "public string LCS(string X, string Y)\n{\n    int m = X.Length;\n    int n = Y.Length;\n    int[,] dp = new int[m + 1, n + 1];\n    for (int i = 0; i < m; i++)\n    {\n        for (int j = 0; j < n; j++)\n        {\n            if (X[i] == Y[j])\n                dp[i + 1, j + 1] = dp[i, j] + 1;\n            else\n                dp[i + 1, j + 1] = Math.Max(dp[i + 1, j], dp[i, j + 1]);\n        }\n    }\n    int index = dp[m, n];\n    char[] lcs = new char[index];\n    int a = m, b = n;\n    while (a > 0 && b > 0)\n    {\n        if (X[a - 1] == Y[b - 1])\n        {\n            lcs[--index] = X[a - 1];\n            a--;\n            b--;\n        }\n        else if (dp[a - 1, b] >= dp[a, b - 1])\n            a--;\n        else\n            b--;\n    }\n    return new string(lcs);\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function lcs(X, Y) {\n    const m = X.length, n = Y.length;\n    const dp = Array.from({length: m + 1}, () => Array(n + 1).fill(0));\n    for (let i = 0; i < m; i++) {\n        for (let j = 0; j < n; j++) {\n            if (X[i] === Y[j])\n                dp[i + 1][j + 1] = dp[i][j] + 1;\n            else\n                dp[i + 1][j + 1] = Math.max(dp[i + 1][j], dp[i][j + 1]);\n        }\n    }\n    let i = m, j = n;\n    const lcsStr = [];\n    while (i > 0 && j > 0) {\n        if (X[i - 1] === Y[j - 1]) {\n            lcsStr.push(X[i - 1]);\n            i--;\n            j--;\n        } else if (dp[i - 1][j] >= dp[i][j - 1]) {\n            i--;\n        } else {\n            j--;\n        }\n    }\n    return lcsStr.reverse().join('');\n}" }
                ],
                Category = dynamicProgrammingCategory
            },
            new ()
            {
                Name = "Matrix Chain Multiplication",
                Description = "Determines the most efficient way to multiply a chain of matrices to minimize the total scalar multiplications.",
                Complexity = Complexity.Cubic,
                UseCases = ["Optimizing matrix operations"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def matrix_chain_order(p):\n    n = len(p) - 1\n    m = [[0] * n for _ in range(n)]\n    for l in range(2, n + 1):\n        for i in range(n - l + 1):\n            j = i + l - 1\n            m[i][j] = float('inf')\n            for k in range(i, j):\n                cost = m[i][k] + m[k + 1][j] + p[i] * p[k + 1] * p[j + 1]\n                if cost < m[i][j]:\n                    m[i][j] = cost\n    return m[0][n - 1]" },
                    new CodeBlock { Language = "C#", Code = "public int MatrixChainOrder(int[] p)\n{\n    int n = p.Length - 1;\n    int[,] m = new int[n, n];\n    for (int l = 2; l <= n; l++)\n    {\n        for (int i = 0; i <= n - l; i++)\n        {\n            int j = i + l - 1;\n            m[i, j] = int.MaxValue;\n            for (int k = i; k < j; k++)\n            {\n                int cost = m[i, k] + m[k + 1, j] + p[i] * p[k + 1] * p[j + 1];\n                if (cost < m[i, j])\n                {\n                    m[i, j] = cost;\n                }\n            }\n        }\n    }\n    return m[0, n - 1];\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function matrixChainOrder(p) {\n    const n = p.length - 1;\n    const m = Array.from({ length: n }, () => Array(n).fill(0));\n    for (let l = 2; l <= n; l++) {\n        for (let i = 0; i <= n - l; i++) {\n            const j = i + l - 1;\n            m[i][j] = Infinity;\n            for (let k = i; k < j; k++) {\n                const cost = m[i][k] + m[k + 1][j] + p[i] * p[k + 1] * p[j + 1];\n                if (cost < m[i][j]) {\n                    m[i][j] = cost;\n                }\n            }\n        }\n    }\n    return m[0][n - 1];\n}" }
                ],
                Category = dynamicProgrammingCategory
            },
            new ()
            {
                Name = "Fibonacci with Memoization",
                Description = "Computes Fibonacci numbers efficiently by caching previously computed values.",
                Complexity = Complexity.Linear,
                UseCases = ["Recursive optimization", "Performance improvement"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def fibonacci(n, memo={}):\n    if n in memo:\n        return memo[n]\n    if n <= 1:\n        return n\n    memo[n] = fibonacci(n-1, memo) + fibonacci(n-2, memo)\n    return memo[n]" },
                    new CodeBlock { Language = "C#", Code = "public int Fibonacci(int n, Dictionary<int, int>? memo = null)\n{\n    if (memo == null)\n        memo = new Dictionary<int, int>();\n    if (memo.ContainsKey(n))\n        return memo[n];\n    if (n <= 1)\n        return n;\n    int result = Fibonacci(n - 1, memo) + Fibonacci(n - 2, memo);\n    memo[n] = result;\n    return result;\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function fibonacci(n, memo = {}) {\n    if (n in memo)\n        return memo[n];\n    if (n <= 1)\n        return n;\n    memo[n] = fibonacci(n - 1, memo) + fibonacci(n - 2, memo);\n    return memo[n];\n}" }
                ],
                Category = dynamicProgrammingCategory
            }
        };

        await aRepository.AddRangeAsync(dynamicProgrammingAlgorithms);

        #endregion

        #region Greedy

        var greedyCategory = new AlgorithmCategory
        {
            Name = "Greedy",
            Description = "Algorithms that make locally optimal choices at each step to find a global optimum."
        };

        var greedyAlgorithms = new Algorithm[]
        {
            new ()
            {
                Name = "Activity Selection",
                Description = "Selects the maximum number of non-overlapping activities by choosing the next activity that finishes earliest.",
                Complexity = Complexity.Linearithmic,
                UseCases = ["Scheduling", "Resource allocation"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def activity_selection(activities):\n    sorted_activities = sorted(activities, key=lambda x: x[1])\n    selected = []\n    last_finish = 0\n    for activity in sorted_activities:\n        if activity[0] >= last_finish:\n            selected.append(activity)\n            last_finish = activity[1]\n    return selected" },
                    new CodeBlock { Language = "C#", Code = "public List<Activity> ActivitySelection(List<Activity> activities)\n{\n    activities.Sort((a, b) => a.Finish.CompareTo(b.Finish));\n    var selected = new List<Activity>();\n    int lastFinish = 0;\n    foreach (var activity in activities)\n    {\n        if (activity.Start >= lastFinish)\n        {\n            selected.Add(activity);\n            lastFinish = activity.Finish;\n        }\n    }\n    return selected;\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function activitySelection(activities) {\n    activities.sort((a, b) => a.finish - b.finish);\n    const selected = [];\n    let lastFinish = 0;\n    for (const activity of activities) {\n        if (activity.start >= lastFinish) {\n            selected.push(activity);\n            lastFinish = activity.finish;\n        }\n    }\n    return selected;\n}" }
                ],
                Category = greedyCategory
            },
            new ()
            {
                Name = "Huffman Coding",
                Description = "Constructs an optimal prefix code for a set of symbols based on their frequencies.",
                Complexity = Complexity.Linearithmic,
                UseCases = ["Data compression", "Encoding"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def huffman_coding(frequencies):\n    import heapq\n    heap = [[freq, [sym, \"\"]] for sym, freq in frequencies.items()]\n    heapq.heapify(heap)\n    while len(heap) > 1:\n        lo = heapq.heappop(heap)\n        hi = heapq.heappop(heap)\n        for pair in lo[1:]:\n            pair[1] = \"0\" + pair[1]\n        for pair in hi[1:]:\n            pair[1] = \"1\" + pair[1]\n        new_node = [lo[0] + hi[0]] + lo[1:] + hi[1:]\n        heapq.heappush(heap, new_node)\n    result = {}\n    if heap:\n        for sym, code in heap[0][1:]:\n            result[sym] = code\n    return result" },
                    new CodeBlock { Language = "C#", Code = "public void HuffmanCoding(Dictionary<char, int> frequencies)\n{\n    var priorityQueue = new SortedSet<HuffmanNode>(Comparer<HuffmanNode>.Create((x, y) => x.Frequency == y.Frequency ? x.Symbol.CompareTo(y.Symbol) : x.Frequency.CompareTo(y.Frequency)));\n    foreach (var kv in frequencies)\n    {\n        priorityQueue.Add(new HuffmanNode { Symbol = kv.Key, Frequency = kv.Value });\n    }\n    while (priorityQueue.Count > 1)\n    {\n        var left = priorityQueue.Min;\n        priorityQueue.Remove(left);\n        var right = priorityQueue.Min;\n        priorityQueue.Remove(right);\n        var parent = new HuffmanNode { Frequency = left.Frequency + right.Frequency, Left = left, Right = right };\n        priorityQueue.Add(parent);\n    }\n    var root = priorityQueue.Min;\n    var codes = new Dictionary<char, string>();\n    BuildCodes(root, \"\");\n    void BuildCodes(HuffmanNode node, string code)\n    {\n        if (node.Left == null && node.Right == null)\n        {\n            codes[node.Symbol] = code;\n            return;\n        }\n        if (node.Left != null) BuildCodes(node.Left, code + \"0\");\n        if (node.Right != null) BuildCodes(node.Right, code + \"1\");\n    }\n}\n\npublic class HuffmanNode\n{\n    public char Symbol { get; set; }\n    public int Frequency { get; set; }\n    public HuffmanNode Left { get; set; }\n    public HuffmanNode Right { get; set; }\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function huffmanCoding(frequencies) {\n    const heap = [];\n    for (const sym in frequencies) {\n        heap.push({ symbol: sym, freq: frequencies[sym] });\n    }\n    heap.sort((a, b) => a.freq - b.freq);\n    while (heap.length > 1) {\n        const left = heap.shift();\n        const right = heap.shift();\n        const parent = { symbol: null, freq: left.freq + right.freq, left: left, right: right };\n        heap.push(parent);\n        heap.sort((a, b) => a.freq - b.freq);\n    }\n    const root = heap[0];\n    const codes = {};\n    function buildCodes(node, code) {\n        if (!node.left && !node.right) {\n            codes[node.symbol] = code;\n            return;\n        }\n        if (node.left) buildCodes(node.left, code + \"0\");\n        if (node.right) buildCodes(node.right, code + \"1\");\n    }\n    buildCodes(root, \"\");\n    return codes;\n}" }
                ],
                Category = greedyCategory
            },
            new ()
            {
                Name = "Fractional Knapsack",
                Description = "Solves the fractional knapsack problem by selecting items based on the highest value-to-weight ratio.",
                Complexity = Complexity.Linearithmic,
                UseCases = ["Resource allocation", "Optimization"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def fractional_knapsack(items, capacity):\n    items.sort(key=lambda x: x[0] / x[1], reverse=True)\n    total_value = 0\n    for value, weight in items:\n        if capacity == 0:\n            break\n        if weight <= capacity:\n            total_value += value\n            capacity -= weight\n        else:\n            fraction = capacity / weight\n            total_value += value * fraction\n            capacity = 0\n    return total_value" },
                    new CodeBlock { Language = "C#", Code = "public double FractionalKnapsack(List<Item> items, int capacity)\n{\n    var sortedItems = items.OrderByDescending(i => (double)i.Value / i.Weight).ToList();\n    double totalValue = 0;\n    foreach (var item in sortedItems)\n    {\n        if (capacity == 0) break;\n        if (item.Weight <= capacity)\n        {\n            totalValue += item.Value;\n            capacity -= item.Weight;\n        }\n        else\n        {\n            double fraction = (double)capacity / item.Weight;\n            totalValue += item.Value * fraction;\n            capacity = 0;\n        }\n    }\n    return totalValue;\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function fractionalKnapsack(items, capacity) {\n    items.sort((a, b) => (b.value / b.weight) - (a.value / a.weight));\n    let totalValue = 0;\n    for (let i = 0; i < items.length && capacity > 0; i++) {\n        if (items[i].weight <= capacity) {\n            totalValue += items[i].value;\n            capacity -= items[i].weight;\n        } else {\n            let fraction = capacity / items[i].weight;\n            totalValue += items[i].value * fraction;\n            capacity = 0;\n        }\n    }\n    return totalValue;\n}" }
                ],
                Category = greedyCategory
            }
        };

        await aRepository.AddRangeAsync(greedyAlgorithms);

        #endregion

        #region Backtracking

        var backtrackingCategory = new AlgorithmCategory
        {
            Name = "Backtracking",
            Description = "Algorithms that incrementally build candidates for solutions and abandon a candidate as soon as it is determined that the candidate cannot possibly lead to a valid solution."
        };

        var backtrackingAlgorithms = new Algorithm[]
        {
            new ()
            {
                Name = "N-Queens Problem",
                Description = "Places N queens on an N×N chessboard so that no two queens threaten each other.",
                Complexity = Complexity.Exponential,
                UseCases = ["Puzzle solving", "Constraint satisfaction"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def solve_n_queens(n):\n    def is_valid(board, row, col):\n        for i in range(row):\n            if board[i] == col or abs(board[i] - col) == row - i:\n                return False\n        return True\n    def backtrack(row, board, solutions):\n        if row == n:\n            sol = []\n            for i in range(n):\n                line = ['.'] * n\n                line[board[i]] = 'Q'\n                sol.append(''.join(line))\n            solutions.append(sol)\n            return\n        for col in range(n):\n            if is_valid(board, row, col):\n                board[row] = col\n                backtrack(row + 1, board, solutions)\n    solutions = []\n    board = [0] * n\n    backtrack(0, board, solutions)\n    return solutions" },
                    new CodeBlock { Language = "C#", Code = "public void SolveNQueens(int n)\n{\n    int[] board = new int[n];\n    Solve(0, n, board);\n}\nprivate void Solve(int row, int n, int[] board)\n{\n    if (row == n)\n    {\n        PrintSolution(board, n);\n        return;\n    }\n    for (int col = 0; col < n; col++)\n    {\n        if (IsValid(board, row, col))\n        {\n            board[row] = col;\n            Solve(row + 1, n, board);\n        }\n    }\n}\nprivate bool IsValid(int[] board, int row, int col)\n{\n    for (int i = 0; i < row; i++)\n    {\n        if (board[i] == col || Math.Abs(board[i] - col) == row - i)\n            return false;\n    }\n    return true;\n}\nprivate void PrintSolution(int[] board, int n)\n{\n    for (int i = 0; i < n; i++)\n    {\n        for (int j = 0; j < n; j++)\n        {\n            Console.Write(board[i] == j ? \"Q\" : \".\");\n        }\n        Console.WriteLine();\n    }\n    Console.WriteLine();\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function solveNQueens(n) {\n    const solutions = [];\n    const board = Array.from({ length: n }, () => Array(n).fill('.'));\n    function isValid(row, col) {\n        for (let i = 0; i < row; i++) {\n            if (board[i][col] === 'Q') return false;\n        }\n        for (let i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--) {\n            if (board[i][j] === 'Q') return false;\n        }\n        for (let i = row - 1, j = col + 1; i >= 0 && j < n; i--, j++) {\n            if (board[i][j] === 'Q') return false;\n        }\n        return true;\n    }\n    function solve(row) {\n        if (row === n) {\n            solutions.push(board.map(r => r.join('')));\n            return;\n        }\n        for (let col = 0; col < n; col++) {\n            if (isValid(row, col)) {\n                board[row][col] = 'Q';\n                solve(row + 1);\n                board[row][col] = '.';\n            }\n        }\n    }\n    solve(0);\n    return solutions;\n}" }
                ],
                Category = backtrackingCategory
            },
            new ()
            {
                Name = "Sudoku Solver",
                Description = "Solves Sudoku puzzles using backtracking to fill the grid according to Sudoku rules.",
                Complexity = Complexity.Exponential,
                UseCases = ["Puzzle solving", "Constraint satisfaction"],
                CodeBlocks =
                [
                    new CodeBlock { Language = "Python", Code = "def solve_sudoku(board):\n    def is_valid(board, row, col, num):\n        for i in range(9):\n            if board[row][i] == num or board[i][col] == num:\n                return False\n        start_row, start_col = 3 * (row // 3), 3 * (col // 3)\n        for i in range(start_row, start_row + 3):\n            for j in range(start_col, start_col + 3):\n                if board[i][j] == num:\n                    return False\n        return True\n\n    def solve():\n        for i in range(9):\n            for j in range(9):\n                if board[i][j] == 0:\n                    for num in range(1, 10):\n                        if is_valid(board, i, j, num):\n                            board[i][j] = num\n                            if solve():\n                                return True\n                            board[i][j] = 0\n                    return False\n        return True\n\n    return solve()" },
                    new CodeBlock { Language = "C#", Code = "public bool SolveSudoku(int[,] board)\n{\n    for (int i = 0; i < 9; i++)\n    {\n        for (int j = 0; j < 9; j++)\n        {\n            if (board[i, j] == 0)\n            {\n                for (int num = 1; num <= 9; num++)\n                {\n                    if (IsValid(board, i, j, num))\n                    {\n                        board[i, j] = num;\n                        if (SolveSudoku(board))\n                            return true;\n                        board[i, j] = 0;\n                    }\n                }\n                return false;\n            }\n        }\n    }\n    return true;\n}\n\nprivate bool IsValid(int[,] board, int row, int col, int num)\n{\n    for (int i = 0; i < 9; i++)\n    {\n        if (board[row, i] == num || board[i, col] == num)\n            return false;\n    }\n    int startRow = (row / 3) * 3;\n    int startCol = (col / 3) * 3;\n    for (int i = 0; i < 3; i++)\n    {\n        for (int j = 0; j < 3; j++)\n        {\n            if (board[startRow + i, startCol + j] == num)\n                return false;\n        }\n    }\n    return true;\n}" },
                    new CodeBlock { Language = "JavaScript", Code = "function solveSudoku(board) {\n    function isValid(board, row, col, num) {\n        for (let i = 0; i < 9; i++) {\n            if (board[row][i] === num || board[i][col] === num) return false;\n        }\n        const startRow = Math.floor(row / 3) * 3;\n        const startCol = Math.floor(col / 3) * 3;\n        for (let i = 0; i < 3; i++) {\n            for (let j = 0; j < 3; j++) {\n                if (board[startRow + i][startCol + j] === num) return false;\n            }\n        }\n        return true;\n    }\n    function solve() {\n        for (let i = 0; i < 9; i++) {\n            for (let j = 0; j < 9; j++) {\n                if (board[i][j] === 0) {\n                    for (let num = 1; num <= 9; num++) {\n                        if (isValid(board, i, j, num)) {\n                            board[i][j] = num;\n                            if (solve()) return true;\n                            board[i][j] = 0;\n                        }\n                    }\n                    return false;\n                }\n            }\n        }\n        return true;\n    }\n    return solve();\n}" }
                ],
                Category = backtrackingCategory
            }
        };

        await aRepository.AddRangeAsync(backtrackingAlgorithms);

        #endregion
    }
}