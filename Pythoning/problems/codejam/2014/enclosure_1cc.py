from itertools import combinations


def calc_closed_points(n, m, inner_coordinates):
    board = [[0] * m for _ in range(n)]

    for (i, j) in inner_coordinates:
        board[i][j] = 1
        board[i][j - 1] = 1
        board[i][j + 1] = 1
        board[i - 1][j] = 1
        board[i + 1][j] = 1

    cnt = sum((1 for i in range(n) for j in range(m) if board[i][j] == 1))

    return cnt


def solve_three(k):
    # Here n == 3

    if k <= 4:
        return k

    if k == 5:
        return 4

    if k <= 7:
        return 6

    if k % 3 == 0:
        return 2 * k // 3 + 1

    return 2 * ((k + 2) // 3)


def set_weight(board, i, j, neighboors1, neighboors2):
    weight = \
        sum((1 for (di, dj) in neighboors1 if board[i + di][j + dj] == 10)) + \
        sum((2 for (di, dj) in neighboors2 if board[i + di][j + dj] == 10))
    board[i][j] = weight


def solve_greedy(n, m, k):
    if n <= 2 or m <= 2:
        return k

    if k >= n * m - 4:
        p = 2 * (n + m - 2)
        return k - n * m + p

    if n == 3 or m == 3:
        return solve_three(k)

    ci = n // 2
    cj = m // 2

    starts = [
        [(ci, cj)],
        [(ci, cj), (ci, cj - 1)],
        [(ci, cj), (ci - 1, cj)],
        [(ci, cj), (ci - 1, cj), (ci, cj - 1), (ci - 1, cj - 1)]
    ]

    answers = [solve_greedy_with_start(n, m, k, start) for start in starts]

    return min(answers)


def solve_greedy_with_start(n, m, k, start):
    neighboors1 = [(0, -1), (0, 1), (-1, 0), (1, 0)]
    neighboors2 = [(1, 1), (-1, 1), (1, -1), (-1, -1)]
    all_neighboors = [(0, -1), (0, 1), (-1, 0), (1, 0), (1, 1), (-1, 1), (1, -1), (-1, -1)]

    board = [[0] * m for _ in range(n)]

    inner_coordinates = []
    for pair in start:
        board[pair[0]][pair[1]] = 10

        inner_coordinates.append(pair)

        cnt = calc_closed_points(n, m, inner_coordinates)
        if cnt >= k:
            return min(cnt - len(inner_coordinates), k)

    while True:
        for dist in range(1, 2 ** 10):

            candidates = []

            for i in range(n):
                for j in range(m):
                    if 0 < i < n - 1 and 0 < j < m - 1 and board[i][j] != 10:
                        for ni, nj in neighboors1:
                            if board[ni + i][nj + j] == 10:
                                candidates.append((i, j))
                                break

            for (candi, candj) in candidates:
                set_weight(board, candi, candj, neighboors1, neighboors2)

            while len(candidates) > 0:

                weight4index = -1

                for i in range(len(candidates)):
                    candi, candj = candidates[i]
                    weight = board[candi][candj]

                    if weight == 4:
                        weight4index = i

                    if weight != 4 and weight != 1:
                        break

                else:
                    if weight4index != -1:
                        i = weight4index
                    else:
                        i = len(candidates) - 1

                candi, candj = candidates[i]

                board[candi][candj] = 10

                inner_coordinates.append((candi, candj))

                candidates.pop(i)

                for (ni, nj) in all_neighboors:
                    nni, nnj = ni + candi, nj + candj
                    if 0 < nni < n - 1 and 0 < nnj < m - 1:
                        weight = board[nni][nnj]
                        if weight != 10:
                            set_weight(board, nni, nnj, neighboors1, neighboors2)

                # print(candidates)
                # print(inner_coordinates)
                # print(board)

                cnt = calc_closed_points(n, m, inner_coordinates)
                if cnt >= k:
                    return min(cnt - len(inner_coordinates), k)

    return -1


def solve_brute(n, m, k):
    if n <= 2 or m <= 2:
        return k, []

    if k >= n * m - 4:
        p = 2 * (n + m - 2)
        return k - n * m + p, []

    inner_coordinates = [(i, j) for i in range(1, n - 1) for j in range(1, m - 1)]

    best = []
    best_cnt = 2 ** 10

    for inner_cnt in range(1, k + 1):
        for tryme in combinations(inner_coordinates, inner_cnt):
            cnt = calc_closed_points(n, m, tryme)
            if cnt >= k:
                if cnt - inner_cnt < best_cnt:
                    best_cnt = cnt - inner_cnt
                    best = tryme
                    break

    if best_cnt > k:
        return k, []

    return best_cnt, best


def test_a_lot():
    for n in range(3, 8):
        for m in range(3, 8):
            for k in range(1, n * m):
                (brute, brute_inner) = solve_brute(n, m, k)

                greedy = solve_greedy(n, m, k)

                assert greedy == brute, "Expected {} actual {}, n,m,k = {},{},{}. Best brute inner {}" \
                    .format(brute, greedy, n, m, k, brute_inner)

                print("{}, {}, {} - {}".format(n, m, k, greedy))


def go(is_debug):
    with open("/Users/Excellent/Documents/Vata/C-large-practice.in") as fin:
        cases = int(fin.readline())

        for case in range(cases):
            (n, m, k) = tuple([int(i) for i in fin.readline().split()])

            answer = solve_greedy(n, m, k)

            if is_debug and (n * m <= 50 or n <= 2 or m <= 2):
                (brute, brute_inner) = solve_brute(n, m, k)

                assert answer == brute, "Expected {} actual {}, n,m,k = {},{},{}. Best brute inner {}" \
                    .format(brute, answer, n, m, k, brute_inner)

                print("Case #{}: {}  ++".format(case + 1, answer))

            else:
                print("Case #{}: {}".format(case + 1, answer))


go(False)
# test_a_lot()
