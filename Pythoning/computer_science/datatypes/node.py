import os


class Node():
    """
    Represent node of the tree, with value
    """

    def __init__(self, value, *children):
        """
        Constructor of the node.

        Usage:
            Node('node', 'child1', 'child2', Node('child3', 'a', 'b'), 'child4')
        """

        self.value = value
        self.children = [(Node(v) if isinstance(v, type(value)) else v) for v in children]

    def get_strings(self, prefix=''):
        """
        Iterate through string representations of the tree

        :param prefix: string that will prefix all the representations
        """
        yield prefix + str(self.value) + ':'

        for child in self.children:
            for s in child.get_strings(prefix + '  '):
                yield s

    def __repr__(self):
        return os.linesep.join(self.get_strings())


if __name__ == '__main__':
    print(Node(1, Node(2, 3, 4, 5, 6), Node(7, Node(8, 9, 10, 11))))