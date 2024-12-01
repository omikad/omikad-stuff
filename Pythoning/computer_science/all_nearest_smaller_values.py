import unittest


def solve(arr):

    stack = []
    result = []

    for x in arr:
        while len(stack) > 0 and stack[-1] > x:
            stack.pop()

        if len(stack) == 0:
            result.append('-')
        else:
            result.append(stack[-1])

        stack.append(x)

    return result


class Tester(unittest.TestCase):

    def test_from_wiki(self):
        arr = [0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15]
        expected = ['-', 0, 0, 4, 0, 2, 2, 6, 0, 1, 1, 5, 1, 3, 3, 7]
        self.assertSequenceEqual(expected, solve(arr))