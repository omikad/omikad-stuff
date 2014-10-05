import unittest


def solve_kadane(arr):
    """
    Dynamic Programming approach:

    P[i] - maximum sum sub array in range [0 .. i]
    Last element index i is either included or not

    Q[i] - maximum sum sub array in range [0 .. i] which has element i included
    R[i] - maximum sum sub array in range [0 .. i] which has element i not included

    => R[i] = P[i - 1]

    => Q[i] = max( 0, Q[i - 1] + arr[i] )

    => P[i] = max( Q[i], R[i] ) = max( Q[i], P[i - 1] )
    """

    max_ending_here = 0  # Q
    max_so_far = 0  # P

    ending_here_start = 0
    max_start = 0
    max_end = 0

    for i, x in enumerate(arr):

        if max_ending_here < 0:
            max_ending_here = x
            ending_here_start = i
        else:
            max_ending_here += x

        if max_ending_here >= max_so_far:
            max_so_far = max_ending_here
            max_start = ending_here_start
            max_end = i
        else:
            pass

    return arr[max_start: max_end + 1]


class Tester(unittest.TestCase):
    def test_empty(self):
        self.assertSequenceEqual([], solve_kadane([]))

    def test_from_wikipedia(self):
        self.assertSequenceEqual([4, -1, 2, 1], solve_kadane([-2, 1, -3, 4, -1, 2, 1, -5, 4]))