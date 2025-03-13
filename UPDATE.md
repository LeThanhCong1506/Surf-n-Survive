# Cập nhật tính năng "Hold nút ngồi"

## Mô tả tính năng
Cập nhật này cho phép nhân vật duy trì trạng thái ngồi xuống khi người chơi giữ phím mũi tên xuống, và nhân vật sẽ đứng dậy khi người chơi nhả phím.

## Các thay đổi chính

### 1. Cập nhật Animator Controller
- Tách animation ban đầu thành 3 trạng thái riêng biệt:
  - **Sit_Down**: Animation ngồi xuống
  - **Sitting**: Trạng thái duy trì khi đang ngồi
  - **Stand_Up**: Animation đứng dậy
- Thiết lập các transitions mới:
  - Idle → Sit_Down (khi Bend = true)
  - Sit_Down → Sitting (khi animation hoàn thành)
  - Sitting → Stand_Up (khi Bend = false)
  - Stand_Up → Idle (khi animation hoàn thành)

### 2. Cập nhật script PlayerController.cs
- Thêm biến `isStandingUp` để theo dõi khi nhân vật đang trong quá trình đứng lên
- Tách xử lý collider:
  - Khi ngồi xuống: Điều chỉnh collider ngay lập tức
  - Khi đứng lên: Khôi phục collider chỉ sau khi animation đứng lên hoàn thành
- Thêm hàm `OnStandComplete()` để reset collider khi animation đứng lên kết thúc
- Thêm logic kiểm tra khi animation đứng lên đã hoàn thành

### 3. Tối ưu hóa cách sử dụng animation
- Loại bỏ việc dừng animation ở frame cụ thể
- Sử dụng states và transitions của Animator Controller để điều khiển animation
- Đảm bảo animation chạy mượt mà khi chuyển đổi giữa các trạng thái

## Cách sử dụng
- **Nhấn và giữ phím mũi tên xuống**: Nhân vật sẽ ngồi xuống và duy trì trạng thái ngồi
- **Nhả phím mũi tên xuống**: Nhân vật sẽ đứng dậy

## Lưu ý khi phát triển tiếp
- Có thể cần điều chỉnh thời gian transitions để animation mượt mà hơn
- Có thể thêm các hiệu ứng âm thanh cho các trạng thái ngồi xuống/đứng lên
- Nếu collider thay đổi không đúng thời điểm, có thể cần điều chỉnh giá trị `normalizedTime` trong script 