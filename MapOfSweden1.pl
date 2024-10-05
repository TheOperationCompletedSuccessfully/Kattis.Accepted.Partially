readUInt(Number):-readUInt(-1,Number).
readUInt(-1,R):-!,get0(Ch),(Ch <48,!,readUInt(-1,R);I1 is Ch-48,readUInt(I1,R)).
readUInt(I,R):-get0(Ch),(Ch <48,!,R is I;I1 is I*10+Ch-48,readUInt(I1,R)).

isLand(35).
isLand(83).

readPending:-get0(Ch),(Ch is 10,!,true;readPending).

enqueue(Index,Row,Col):-(visited(Row,Col),!,true;queue(_,Row,Col),!,true;assert(queue(Index,Row,Col))).


readRow(Cols,Cols,_).
readRow(I,Cols,Row):-II is I+1,get0(Ch),(Ch=\=46,!,assert(data(Row,II,Ch)),totalLand(Old),retract(totalLand(Old)),New is Old+1,assert(totalLand(New));true),readRow(II,Cols,Row).

readData(Rows,Rows,_).
readData(I,Rows,Cols):-fill_buffer(user_input),II is I+1,readRow(0,Cols,II),readPending,readData(II,Rows,Cols).

moveUp(Row,Col,I):-NewRow is Row-1,(NewRow>0,!,enqueue(I,NewRow,Col);true).
moveLeft(Row,Col,I):-NewCol is Col-1,(NewCol>0,!,enqueue(I,Row,NewCol);true).
moveDown(Row,Col,I,Rows):-NewRow is Row+1,(NewRow=<Rows,!,enqueue(I,NewRow,Col);true).
moveRight(Row,Col,I,Cols):-NewCol is Col+1,(NewCol=<Cols,!,enqueue(I,Row,NewCol);true).


performMoves(Row,Col,I,Rows,Cols):-moveUp(Row,Col,I),moveDown(Row,Col,I,Rows),moveLeft(Row,Col,I),moveRight(Row,Col,I,Cols).

processQueue(N,N,_,_).
processQueue(I,N,Rows,Cols):-II is I+1,(queue(I,Row,Col),!,retract(queue(I,Row,Col)),(data(Row,Col,_),!,collected(Old),retract(collected(Old)),New is Old+1,assert(collected(New)),(visited(Row,Col),!,true;assert(visited(Row,Col)),performMoves(Row,Col,II,Rows,Cols));true),processQueue(I,N,Rows,Cols);queue(II,_,_),!,processQueue(II,N,Rows,Cols);true).

assertDefaults:-assert(data(-1,-1,-1)),assert(queue(-1,-1,-1)),assert(visited(-1,-1)),assert(collected(0)).

checkNewTraverse(R,C):-RR is R+1,data(RR,C,_),not(visited(RR,C)),not(queue(_,RR,C)),!,true.
checkNewTraverse(R,C):-RR is R-1,data(RR,C,_),not(visited(RR,C)),not(queue(_,RR,C)),!,true.
checkNewTraverse(R,C):-CC is C+1,data(R,CC,_),not(visited(R,CC)),not(queue(_,R,CC)),!,true.
checkNewTraverse(R,C):-CC is C-1,data(R,CC,_),not(visited(R,CC)),not(queue(_,R,CC)),!,true.

logAllData:-findall([R,C],data(R,C,_),L),writeln(L),findall([R,C],visited(R,C),LL),writeln(LL).

checkConnected(R,C):-RR is R+1,data(RR,C,_),visited(RR,C).
checkConnected(R,C):-RR is R-1,data(RR,C,_),visited(RR,C).
checkConnected(R,C):-CC is C+1,data(R,CC,_),visited(R,CC).
checkConnected(R,C):-CC is C-1,data(R,CC,_),visited(R,CC).

bufferedWriteln(C):-with_output_to(user_output,writeln(C)).

processQueries(N,N,_,_,_).
processQueries(_,_,_,_,_):-totalLand(Land),collected(Land).
processQueries(I,N,Old,Rows,Cols):-II is I+1, readUInt(R),readUInt(C),RC is Rows*Cols-Old,assert(data(R,C,35)),(checkConnected(R,C),(checkNewTraverse(R,C),!,enqueue(0,R,C),processQueue(0,RC,Rows,Cols);Res is Old+1,retractall(collected(_)),assert(collected(Res)),assert(visited(R,C)));true),collected(Res),bufferedWriteln(Res),processQueries(II,N,Res,Rows,Cols).

main:-set_stream(user_input,buffer_size(16384)),assert(totalLand(0)),readUInt(Rows),readUInt(Cols),readUInt(Q),assertDefaults,readData(0,Rows,Cols),data(Row,Col,83),enqueue(0,Row,Col),RC is Rows*Cols,processQueue(0,RC,Rows,Cols),collected(Res),bufferedWriteln(Res),processQueries(0,Q,Res,Rows,Cols).