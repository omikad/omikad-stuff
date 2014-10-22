import unittest
import math


def solve_recursive(i):
    # memory O(n)

    div, mod = divmod(i, 10)
    if div == 0:
        return str(mod)
    else:
        return solve_recursive(div) + str(mod)


def solve_lg(i):
    # memory O(1) [ if string s doesn't count ]

    length = math.floor(math.log(i, 10))
    power = int(math.pow(10, length))
    s = ''
    for x in range(math.floor(length)):
        div, mod = divmod(i, power)
        power //= 10
        s += str(div)
        i = mod
    return s + str(i)


class Tester(unittest.TestCase):

    _tests = [1, 12, 1234567, 9, 99999, 10203, 10002]

    def test_recurse(self):
        for t in self._tests:
            self.assertEqual(str(t), solve_recursive(t))

    def test_lg(self):
        for t in self._tests:
            self.assertEqual(str(t), solve_lg(t))


