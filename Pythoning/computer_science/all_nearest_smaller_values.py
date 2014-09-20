if __name__ == "__main__":
    arr = [0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15]

    stack = []
    result = []

    for x in arr:
        while len(stack) > 0 and stack[-1] > x:
            stack.pop()

        if len(stack) == 0:
            result.append('-')
        else:
            result.append(stack[-1])

        stack.append(x)

    print(result)