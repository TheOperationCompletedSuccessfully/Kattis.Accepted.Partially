readUInt(Number):-readUInt(0,Number).
readUInt(I,R):-get0(Ch),(Ch <48,!,R is I;I1 is I*10+Ch-48,readUInt(I1,R)).

readTooth(Number,LR,BM):-readTooth(0,Number,LR,BM).
readTooth(0,R,LR,BM):-get0(Ch),(Ch <48,!,readTooth(0,R,LR,BM);I1 is Ch-48,readTooth(I1,R,LR,BM)).
readTooth(I,R,LR,BM):-get0(Ch),(Ch <43,!,R is I*8,LR is 1,get0(BM1),(BM1 is 32,get0(BM);BM is BM1),get0(_);Ch <48,!,R is I*8,LR is 0,get0(BM1),(BM1 is 32,get0(BM);BM is BM1),get0(_);I1 is I*10+Ch-48,readTooth(I1,R,LR,BM)).

processTooth(_,0,98,Left,_,NewLeft,NewRight):-!,NewLeft = Left, NewRight = [0,0,0,0,0,0,0,0].
processTooth(T,0,109,Left,Right,NewLeft,NewRight):-select(T,Right,0,NewRight),NewLeft = Left.
processTooth(_,1,98,_,Right,NewLeft,NewRight):-!,NewLeft = [0,0,0,0,0,0,0,0],NewRight = Right.
processTooth(T,1,109,Left,Right,NewLeft,NewRight):-select(T,Left,0,NewLeft),NewRight = Right.

analyzeResults([],[]):-!,writeln(2).
analyzeResults([0|Tail1],Right):-!,analyzeResults(Tail1,Right).
analyzeResults([],[0|Tail2]):-!,analyzeResults([],Tail2).
analyzeResults([Ch|_],_):-!,writeln(0).
analyzeResults([],[Ch|_]):-!,writeln(1).


solve(N,N,Left,Right,Left,Right).
solve(I,N,Left,Right,EndLeft,EndRight):-I1 is I+1,readTooth(T,LR,BM),/*writeln(T),writeln(LR),writeln(BM),*/processTooth(T,LR,BM,Left,Right,NewLeft,NewRight),solve(I1,N,NewLeft,NewRight,EndLeft,EndRight).

main:-Right = [8,16,24,32,40,48,56,64],Left =[8,16,24,32,40,48,56,64],readUInt(Cases),solve(0,Cases,Left,Right,EndLeft,EndRight),analyzeResults(EndLeft,EndRight).