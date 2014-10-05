from queue import Queue
import unittest
from computer_science.datatypes.binary_node import BinaryNode


def dfs(root):
    stack = [root]
    while len(stack) > 0:
        v = stack.pop()
        yield v
        for child in v.children():
            stack.append(child)


def bfs(root):
    que = Queue()
    que.put(root)
    while not que.empty():
        v = que.get()
        yield v
        for child in v.children():
            que.put(child)


def preorder(root):
    yield root
    if root.left is not None:
        for child in preorder(root.left):
            yield child
    if root.right is not None:
        for child in preorder(root.right):
            yield child


def inorder(root):
    if root.left is not None:
        for child in inorder(root.left):
            yield child
    yield root
    if root.right is not None:
        for child in inorder(root.right):
            yield child


def postorder(root):
    if root.left is not None:
        for child in postorder(root.left):
            yield child
    if root.right is not None:
        for child in postorder(root.right):
            yield child
    yield root


class Tester(unittest.TestCase):
    def _get_tree(self):
        return BinaryNode(7,
                          BinaryNode(1,
                                     0,
                                     BinaryNode(3,
                                                2,
                                                BinaryNode(5, 4, 6))),
                          BinaryNode(9,
                                     8,
                                     BinaryNode(10, None, 11)))

    def _values(self, sequence):
        return [n.value for n in sequence]

    def test_dfs(self):
        self.assertSequenceEqual([7, 9, 10, 11, 8, 1, 3, 5, 6, 4, 2, 0], self._values(dfs(self._get_tree())))

    def test_bfs(self):
        self.assertSequenceEqual([7, 1, 9, 0, 3, 8, 10, 2, 5, 11, 4, 6], self._values(bfs(self._get_tree())))

    def test_preorder(self):
        self.assertSequenceEqual([7, 1, 0, 3, 2, 5, 4, 6, 9, 8, 10, 11], self._values(preorder(self._get_tree())))

    def test_inorder(self):
        self.assertSequenceEqual([0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11], self._values(inorder(self._get_tree())))

    def test_postorder(self):
        self.assertSequenceEqual([0, 2, 4, 6, 5, 3, 1, 8, 11, 10, 9, 7], self._values(postorder(self._get_tree())))

