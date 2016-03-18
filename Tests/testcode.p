im GL
im Standard

pt 1

calib GL clear
calib GL begin

set 0 BYTE 0
set 1 BYTE 0
set 2 BYTE 0
calib GL clear_color 0 1 2

set 0 BYTE 255
set 1 BYTE 0
set 2 BYTE 0
set 3 DOUBLE -1
set 4 DOUBLE 1
calib GL color 0 1 2
calib GL vertex_d 3 4

set 0 BYTE 255
set 1 BYTE 0
set 2 BYTE 255
set 3 DOUBLE 1
set 4 DOUBLE 1
calib GL color 0 1 2
calib GL vertex_d 3 4

set 0 BYTE 0
set 1 BYTE 255
set 2 BYTE 0
set 3 DOUBLE 1
set 4 DOUBLE -1
calib GL color 0 1 2
calib GL vertex_d 3 4

set 0 BYTE 0
set 1 BYTE 0
set 2 BYTE 255
set 3 DOUBLE -1
set 4 DOUBLE -1
calib GL color 0 1 2
calib GL vertex_d 3 4

calib GL end
calib GL SWAP_BUFFER
mov 1