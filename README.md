🕹️ Giới thiệu Game "Treasure Hunter" 

"Treasure Hunter" là một game phiêu lưu 2D được phát triển bằng Unity, mang phong cách platformer cổ điển nhưng đầy thử thách. Trò chơi có hệ thống gameplay rõ ràng, được tổ chức tốt với các mẫu thiết kế phần mềm (design pattern) hiện đại để đảm bảo mở rộng và bảo trì dễ dàng.

🎯 Tính năng nổi bật

✅ Thiết kế theo chuẩn OOP với các pattern hiện đại:

Sử dụng Singleton cho các lớp quản lý như GameManager, AudioManager, UIManager.

Triển khai Observer Pattern để cập nhật UI theo thời gian thực (ví dụ: điểm số, trạng thái game).

🧩 Hệ thống màn chơi (Levels):

Cấu trúc mở khóa từng cấp: Chỉ khi vượt qua màn chơi trước, bạn mới có thể tiếp tục đến màn tiếp theo.

Tilemap được sử dụng để xây dựng bản đồ từng màn chơi.

👾 Enemy:

2 loại quái vật với đầy đủ animation (di chuyển, tấn công, chết).

Có khả năng tương tác và bị tấn công từ người chơi.

🧍 Player:

Hỗ trợ các hành động như di chuyển, nhảy, tấn công với đầy đủ animation.

Có thể nhặt item và tương tác với môi trường.

🎯 Gameplay:

Có hệ thống tính điểm từng màn chơi và tổng điểm toàn game.

Lưu điểm và tiến độ vào hệ thống để người chơi quay lại vẫn giữ được trạng thái.

📦 Prefab:

Tối ưu phát triển bằng cách sử dụng Prefab cho các UI, Enemy, Item, và các đối tượng khác.

🎵 Âm thanh & Hiệu ứng:

Có nhạc nền (BGM) và hiệu ứng âm thanh (SFX) cho hành động như nhặt item, chiến đấu, hoàn thành màn chơi...

🧩 UI hoàn chỉnh:

Bao gồm các màn: Main Menu, Chọn màn chơi, Cài đặt, Thắng màn, Thua màn, và Hoàn thành game.

Giao diện thân thiện, hỗ trợ người chơi trải nghiệm mượt mà.
