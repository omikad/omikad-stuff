import unittest


class UnionFind():

    def __init__(self, count):
        """
        Constructor of the union find structure
        """

        self.clusters_count = count
        self._leaders = [i for i in range(count)]
        self._ranks = [0] * count

    def union(self, x, y):
        """
        Put x and y into one cluster
        """

        ot = self.find(x)
        to = self.find(y)

        if ot == to:
            return

        self.clusters_count -= 1

        rank_ot = self._ranks[ot]
        rank_to = self._ranks[to]

        if rank_ot == rank_to:
            self._leaders[ot] = to
            self._ranks[to] += 1

        elif rank_ot < rank_to:
            self._leaders[ot] = to

        else:
            self._leaders[to] = ot

    def find(self, i):
        """
        Find cluster for i
        """

        prev = i
        leader = self._leaders[prev]

        while leader != prev:
            prev = leader
            leader = self._leaders[prev]

        self._leaders[i] = leader

        return leader


class Tester(unittest.TestCase):

    def test_union_find(self):
        union = UnionFind(4)

        self._assert_not_union(union, 0, 1, 2, 3)

        union.union(0, 1)

        self._assert_union(union, 0, 1)
        self._assert_not_union(union, 0, 2, 3)

        union.union(2, 3)
        self._assert_union(union, 0, 1)
        self._assert_union(union, 2, 3)
        self._assert_not_union(union, 0, 2)

        union.union(0, 2)
        self._assert_union(union, 0, 1, 2, 3)

    def _assert_union(self, union, *indices):
        clusters = set((union.find(i) for i in indices))
        self.assertEqual(1, len(clusters))

    def _assert_not_union(self, union, *indices):
        clusters = set((union.find(i) for i in indices))
        self.assertEqual(len(indices), len(clusters))
