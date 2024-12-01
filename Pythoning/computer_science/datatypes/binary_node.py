import os


class BinaryNode():
    """
    Represent binary node of the tree, with value
    """

    def __init__(self, value, left=None, right=None):
        """
        Constructor of the node.
        Usage:
            BinaryNode(42, BinaryNode(...), BinaryNode(...))
            BinaryNode('value', 'left value', 'right value')
        """

        self.value = value
        self.left = BinaryNode(left) if isinstance(left, type(value)) else left
        self.right = BinaryNode(right) if isinstance(right, type(value)) else right

    def children(self):
        """
        Iterate through the children
        """
        if self.left is not None:
            yield self.left
        if self.right is not None:
            yield self.right

    def get_strings(self, prefix=''):
        """
        Iterate through string representations of the tree

        :param prefix: string that will prefix all the representations
        """
        yield prefix + str(self.value) + ':'

        if self.left is None and self.right is None:
            return

        if self.left is not None:
            for s in self.left.get_strings(prefix + '  '):
                yield s
        else:
            yield prefix + '  -'

        if self.right is not None:
            for s in self.right.get_strings(prefix + '  '):
                yield s
        else:
            yield prefix + '  -'

    def __repr__(self):
        return os.linesep.join(self.get_strings())


if __name__ == '__main__':
    print(BinaryNode(4, BinaryNode(2), BinaryNode(6, BinaryNode(5))))
    print()
    print(BinaryNode('root', BinaryNode('root left', 'aaa', 'bbb'), 'root right'))