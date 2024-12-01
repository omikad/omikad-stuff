import string
import unittest


def _reverse(arr, start, end):
    length = end + 1 - start
    to = length // 2
    for i in range(to):
        ii = start + i
        jj = end - i
        arr[ii], arr[jj] = arr[jj], arr[ii]


def solve(s):
    # O(n) time
    # O(1) memory

    if s == '':
        return s

    arr = list(s)

    start = 0
    is_in_word = False
    for i, c in enumerate(arr):
        if c in string.whitespace:
            if is_in_word:
                _reverse(arr, start, i - 1)
            is_in_word = False
        else:
            if not is_in_word:
                start = i
            is_in_word = True

    if is_in_word:
        _reverse(arr, start, len(arr) - 1)

    return ''.join(arr)


class Tester(unittest.TestCase):
    def test_empty(self):
        self.assertEqual('', solve(''))

    def test_one_word(self):
        self.assertEqual('1', solve('1'))
        self.assertEqual('10', solve('01'))
        self.assertEqual('210', solve('012'))
        self.assertEqual('543210', solve('012345'))

    def test_multiple_words(self):
        self.assertEqual(' 0 1 ', solve(' 0 1 '))
        self.assertEqual(' 10 1 ', solve(' 01 1 '))
        self.assertEqual(' 1 21 ', solve(' 1 12 '))
        self.assertEqual(' 210 1', solve(' 012 1'))
        self.assertEqual('210 43210', solve('012 01234'))
