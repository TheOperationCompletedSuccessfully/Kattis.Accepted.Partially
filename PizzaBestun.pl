:-use_module(moduleMyNumbers, [readUInt/1]).
:-use_module(moduleBufferedIO, [initiateBufferedRead/1,bufferedWrite/1,bufferedWriteln/1]).
:-use_module(moduleMinHeapV3,[minHeapPop/2,minHeapPush/3,minHeapPeek/2,minHeapPrintAll/1,minHeap/1,minHeapLength/2]).

solve(N,N,_,Result,Result).
solve(I,N,HeapName,Current,Result):-II is I+1,readUInt(_),readUInt(Next),minHeapPush(HeapName,Next,Next),NewC is Current+Next,solve(II,N,HeapName,NewC,Result).

countData(N,N,_,Result,Result).
countData(I,N,HeapName,Current,Result):-II is I+1,R is I rem 2,minHeapPop(HeapName,H), (R is 0,!,NewC is Current+H;NewC is Current),countData(II,N,HeapName,NewC,Result).

main:-initiateBufferedRead(524288),readUInt(N),minHeap('Pizzas'),solve(0,N,'Pizzas',0,Sum),countData(0,N,'Pizzas',0,Result),bufferedWriteln(Result).