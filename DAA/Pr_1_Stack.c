#include<stdio.h>
#include<stdlib.h>
#define max 8
int stack[max];
int top = -1;

void push(int value){
    if(top == max - 1){
        printf("Stack overflow..\n");
        return;
    }
    else{
        printf("Enter value to push : ");
        scanf("%d", &value);
        top++;
        stack[top] = value;
        printf("value inserted.\n");
        return;
    }
}

void pop(){
    if(top == -1){
        printf("Stack underflow..\n");
        return;
    }
    else{
        printf("%d\n", stack[top]);
        top--;
        return;
    }
}

void peek(){
    if(top == -1){
        printf("Stack underflow..\n");
        return;
    }
    else{
        printf("%d\n", stack[top]);
        return;
    }
}

void display(){
    if(top == -1){
        printf("Stack underflow\n");
        return;
    }
    else{
        int i = top;
        while(i > -1){
            printf("%d\n", stack[i]);
            i--;
        }
        return;
    }
}

void main(){
        int choice, value;
    while(1){
        printf("\n1. Push\n2. Pop\n3. Peep\n4. Display\n5. Exit\n");
        printf("Enter choice : ");
        scanf("%d", &choice);
    
        switch(choice){
            case 1 :
                    push(value);
                    break;

            case 2 :pop();
                    break;

            case 3 :peek();
                    if(value != -1){
                        printf("Element : %d", value);
                    }
                    break;
            case 4 :display();
                    break;
            case 5 :exit(0);

            default:("Invalid choice...\n");

        }
    }
}

