import numpy as np


def solve1(filename):
    with open(filename) as file:
        lines = [line.rstrip() for line in file]
    lines = [l.split() for l in lines]

    block = np.zeros((30, 30, 30), dtype=np.int64)

    for x, y, z in np.ndindex(block.shape):
        stack = []
        cmdi = 0
        while True:
            cmd = lines[cmdi][0]
            if cmd == 'ret':
                break

            if cmd == 'push':
                arg = lines[cmdi][1]
                if arg == 'X':
                    stack.append(x)
                elif arg == 'Y':
                    stack.append(y)
                elif arg == 'Z':
                    stack.append(z)
                else:
                    stack.append(int(arg))

            elif cmd == 'add':
                arg0, arg1 = stack.pop(), stack.pop()
                stack.append(arg0 + arg1)

            elif cmd == 'jmpos':
                arg = stack.pop()
                if arg >= 0:
                    cmdi += int(lines[cmdi][1])

            else:
                print(lines[cmdi])
                raise 1
            
            cmdi += 1

        block[x, y, z] = stack[-1]

    print("Part1:", np.sum(block))

    ans2 = 0
    vis = np.zeros_like(block)
    for sp in np.ndindex(block.shape):
        if vis[sp] == 0 and block[sp] > 0:
            vis[sp] = 1
            ans2 += 1
            Q = [sp]
            qi = 0
            while qi < len(Q):
                p = Q[qi]
                for dr in range(-1, 2):
                    for dc in range(-1, 2):
                        for dz in range(-1, 2):
                            if abs(dr) + abs(dc) + abs(dz) == 1:
                                nextp = (p[0] + dr, p[1] + dc, p[2] + dz)
                                if 0 <= nextp[0] < block.shape[0] and 0 <= nextp[1] < block.shape[1] and 0 <= nextp[2] < block.shape[2]:
                                    if vis[nextp] == 0 and block[nextp] > 0:
                                        vis[nextp] = 1
                                        Q.append(nextp)
                qi += 1

    print("Part2:", ans2)

# solve('input1.txt')
solve1('/home/excellent/Downloads/input.txt')   # 4847


