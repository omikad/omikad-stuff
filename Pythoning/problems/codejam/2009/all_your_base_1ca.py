def solve(s):
    digits = set(s)
    base = max(2, len(digits))

    numbers = {s[0]: 1}

    zero_found = False
    last = 1

    result = 1

    for ci in range(1, len(s)):
        c = s[ci]

        if c in numbers:
            digit = numbers[c]

        elif zero_found:
            last += 1
            numbers[c] = last
            digit = last

        else:
            numbers[c] = 0
            digit = 0
            zero_found = True

        result = result * base + digit

    return result

print(solve("1"))
print(solve("111"))
print(solve("1234"))
print(solve("11001001"))
print(solve("cats"))
print(solve("zig"))
print(solve("iiixy"))

