# sparta_Bank
스파르타 뱅크 개인과제입니다.

## 🛠 사용 기술

### 🔹 프론트엔드 (게임)
- Unity 2022.3.17f1
- C#
- TextMeshPro UI
- Unity Recorder (녹화 기능 사용)

### 🔹 백엔드 (API 서버)
- Node.js (Express)
- MySQL (XAMPP 또는 Local MySQL 서버)
- REST API

## 🎮 Unity 기능 구성

- 로그인 및 회원가입 UI 구성 (Popup 형식)
- 사용자 입력에 따라 API 호출
- 현금(cash)과 은행잔고(bankBalance) 표시
- 입금/출금 시 UI 반응형 처리
- Vertical Layout Group을 활용한 버튼 정렬
- 백엔드 API 연동을 통한 실시간 데이터 반영

## 🧱 MySQL 테이블 정보

```sql
-- DB: atmdb
CREATE TABLE users (
  userName VARCHAR(50) PRIMARY KEY,
  password VARCHAR(100) NOT NULL,
  name VARCHAR(50),
  cash INT,
  bankBalance INT
);

-- 테스트 계정 삽입
INSERT INTO users (userName, password, name, cash, bankBalance)
VALUES ('test123', '1234', '테스트유저', 10000, 50000);