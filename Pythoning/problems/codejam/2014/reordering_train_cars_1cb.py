from functools import reduce
from itertools import permutations
from math import factorial


def check(s):
    forbids = set()
    cur = s[0]
    for c in s:
        if c in forbids:
            return False
        if c != cur:
            forbids.add(cur)
            cur = c
    return True


def solve_brute(strings):
    cnt = 0
    for tryme in permutations(strings):
        line = ''.join(tryme)
        if check(line):
            cnt += 1
    return cnt


class Group(object):
    def __init__(self, s, w):
        self.string = s
        self.weight = w

    def __repr__(self):
        return "'{}' * {}".format(self.string, self.weight)


def solve_grouping(strings):
    chars = set(''.join(strings))

    groups = [Group(s, 1) for s in strings]

    for char in chars:
        char_groups = [g for g in groups if char in g.string]

        start = []
        end = []
        middle = []

        for x in char_groups:
            if x.string[0] != char:
                start.append(x)
            elif x.string[-1] != char:
                end.append(x)
            else:
                middle.append(x)

        if len(start) > 1 or len(end) > 1:
            return 0

        merged = start[0] if len(start) == 1 else Group('', 1)
        if len(middle) > 0:
            merged.string += ''.join((s.string for s in middle))
            merged.weight *= factorial(len(middle))
        if len(end) == 1:
            merged.string += end[0].string
            merged.weight *= end[0].weight

        groups = [g for g in groups if not char in g.string] + [merged]

    for g in groups:
        if not check(g.string):
            return 0

    fact = factorial(len(groups)) % 1000000007
    mult = reduce(lambda acc, elem: (elem.weight * acc) % 1000000007, groups, 1)

    return (fact * mult) % 1000000007


def go(is_debug):
    with open("/Users/Excellent/Documents/Vata/B-large-practice.in") as fin:
        cases = int(fin.readline())

        for case in range(cases):
            n = int(fin.readline())
            strings = fin.readline().split()
            assert n == len(strings), "Expected %d, actual %r" % (n, strings)

            cnt = solve_grouping(strings)

            if is_debug and len(strings) <= 10:
                cnt_brute = solve_brute(strings)

                assert cnt_brute == cnt, 'Expected {}, actual {}. Task: {}'.format(cnt_brute, cnt, strings)

            print("Case #{}: {}".format(case + 1, cnt))
            # print()

go(False)