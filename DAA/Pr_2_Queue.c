#include<stdio.h>
#include<stdlib.h>
#define max 8
int queue[max];
int first = -1, last = -1;

void enqueue(int value){
    if(last >= max-1){
        printf("Queue overflow\n");
        return;
    }
    else{
        last++;
        queue[last] = value;
        if(first == -1){
            first = 0;
        }
        return;
    }
}
void dequeue(){
    int y;
    if(first==-1){
        printf("Queue underflow...\n");
        return;
    }
    else{
        y = queue[first];
        if(first == last){
            first = last = -1;
        }
        else{
            first++;
        }    
    }
    printf("%d\n", y);
}

void display(){
    for(int i = first; i <= last; i++){
        printf("%d\t", queue[i]);
    }
    printf("\n");
}

void main(){
    int choice, value;

    while(1){
        printf("1. Enqueue\n2. Dequeue\n3. display\n4. Exit\n");
        printf("Enter choice : ");
        scanf("%d", &choice);
        switch(choice){
            case 1 :    printf("Enter value to insert : ");
                        scanf("%d", &value);
                        enqueue(value);
                        break;
            case 2 :    dequeue();
                        break;
            case 3 :    display();
                        break;
            case 4 :    exit(0);
            default :   printf("Invalid choice...");

            
        }


    }

}

