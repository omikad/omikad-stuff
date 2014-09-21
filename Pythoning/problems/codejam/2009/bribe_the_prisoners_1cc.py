from itertools import permutations


def calc_money(prisoners, queue):
    neighboors = {i: [i - 1, prisoners - i] for i in queue}

    money = 0
    for releasme in queue:
        money += neighboors[releasme][0] + neighboors[releasme][1]

        del neighboors[releasme]

        for prisoner in neighboors:

            if prisoner > releasme:
                left = neighboors[prisoner][0]

                if prisoner - left <= releasme:
                    neighboors[prisoner][0] = prisoner - releasme - 1

            elif prisoner < releasme:
                right = neighboors[prisoner][1]

                if prisoner + right >= releasme:
                    neighboors[prisoner][1] = releasme - prisoner - 1

    return money


def get_money_dp(prisoners, queue):
    # O( ||queue|| ^ 3 )

    queue_sorted = sorted(queue)

    first = []
    for (i, q) in enumerate(queue_sorted):
        if i == 0:
            first.append(q - 1)
        else:
            first.append(q - queue_sorted[i - 1] - 1)
    first.append(prisoners - queue_sorted[-1])

    m = {1: first}

    for task_len in range(2, len(queue) + 2):
        m[task_len] = [0] * (len(queue) - task_len + 2)

        for start in range(len(queue) - task_len + 2):

            length = sum((m[1][start + i] + 1) for i in range(task_len)) - 2

            best_money = 2 ** 20

            for root in range(1, task_len):

                left = 0 if root == 1 else m[root][start]
                right = 0 if root == task_len - 1 else m[task_len - root][start + root]

                money = length + left + right

                if money < best_money:
                    best_money = money

            m[task_len][start] = best_money

    return m[len(queue) + 1][0]


def get_money_bruteforce(prisoners, queue):
    best_money = 2 ** 20

    for tryme in permutations(queue):
        money = calc_money(prisoners, tryme)

        if money < best_money:
            best_money = money

    return best_money


def go(is_debug):
    with open("/Users/Excellent/Documents/Vata/C-large-practice.in") as fin:
        cases = int(fin.readline())

        for case in range(cases):
            pq_arr = [int(i) for i in fin.readline().split()]
            prisoners, queue_len = pq_arr[0], pq_arr[1]

            queue = [int(i) for i in fin.readline().split()]

            money = get_money_dp(prisoners, queue)
            print("Case #{}: {}".format(case + 1, money))

            if is_debug:
                best_money = get_money_bruteforce(prisoners, queue)
                print("Case #{}: {}".format(case + 1, best_money))
                if money != best_money:
                    raise Exception
                print()

go(False)