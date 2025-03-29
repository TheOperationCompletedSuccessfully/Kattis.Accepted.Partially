:-use_module(moduleMyNumbers, [readUInt/1]).
:-use_module(moduleBufferedIO, [initiateBufferedRead/1,bufferedWrite/1,bufferedWriteln/1]).

:-dynamic data/2.

readData(N,N,_):-!,bufferedWriteln('Neibb').
readData(I,N,K):-II is I+1,readUInt(Next),FF is K-Next,(data(_,FF),!,bufferedWrite(Next),bufferedWrite(' '),bufferedWriteln(FF);assert(data(I,Next)),readData(II,N,K)).


main:-initiateBufferedRead(4194304),readUInt(N),readUInt(K),readData(0,N,K).