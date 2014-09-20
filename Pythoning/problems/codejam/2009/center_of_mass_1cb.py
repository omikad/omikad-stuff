with open("/Users/Excellent/Documents/Vata/B-large-practice.in") as fin:
    cases = int(fin.readline())

    for case in range(cases):
        n = int(fin.readline())

        positions = [[], [], []]
        velocities = [[], [], []]

        for i in range(n):
            line = fin.readline()

            numbers = [float(j) for j in line.split()]

            positions[0].append(numbers[0])
            positions[1].append(numbers[1])
            positions[2].append(numbers[2])

            velocities[0].append(numbers[3])
            velocities[1].append(numbers[4])
            velocities[2].append(numbers[5])

        pos = [sum(a) / n for a in positions]
        vel = [sum(a) / n for a in velocities]

        a = sum((pos[i] * vel[i] for i in range(3)))
        b = sum((vel[i] * vel[i] for i in range(3)))

        if b == 0:
            tmin = 0
            dmin = sum((pos[i]) ** 2 for i in range(3)) ** 0.5
        else:
            tmin = a / (-b)
            if tmin < 0.00000001:
                tmin = 0
            dmin = sum(((pos[i] + tmin * vel[i]) ** 2) for i in range(3)) ** 0.5

        print("Case #{0}: {1:.8f} {2:.8f}".format(case + 1, dmin, tmin))
