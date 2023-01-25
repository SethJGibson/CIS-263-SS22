#include <stdio.h>

int input[5];

void insertionSort(int range) {
    int cursor = 0, index = 1;
    int placeHolder = input[1];

    for (index; index < range; index++) {
        placeHolder = input[index];
        cursor = index - 1;
        while ((input[cursor] > placeHolder) && (cursor >= 0)) {
            input[cursor + 1] = input[cursor];
            cursor--;
        }
        input[cursor + 1] = placeHolder;
    }
}

void displayContents(int range) {
    printf("> ");
    for (int i = 0; i < range; i++)
        printf("%d ", input[i]);
    printf("\n");
}

void getInput(int range) {
    printf("%d. ", range);
    scanf("%d", &input[range - 1]);
    insertionSort(range);
    displayContents(range);
}

int main()
{
    int range = 1;
    printf("Please enter one integer at a time.\n");

    for (range; range <= 5; range++)
        getInput(range);

    return 0;
}