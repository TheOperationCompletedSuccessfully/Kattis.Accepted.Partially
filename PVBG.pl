:-use_module(moduleMyNumbers, [readUInt/1]).
:-use_module(moduleBufferedIO, [initiateBufferedRead/1,bufferedWrite/1,bufferedWriteln/1]).

readData(N,N,Result,Result).
readData(I,N,Current,Result):-II is I+1,readUInt(Next), NewC is min(Next,Current),(NewC is 1,!,readData(N,N,1,Result);readData(II,N,NewC,Result)).

main:-initiateBufferedRead(2097152),readUInt(N),readData(0,N,1000000000000000000,Max),Result is Max+1,bufferedWriteln(Result).