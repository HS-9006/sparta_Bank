# sparta_Bank
Node.js + MySQL 백엔드와 연동하여 유저 데이터(예금, 현금, 사용자명 등)를 처리합니다.

### 🛠 사용 기술
- Node.js (Express)
- MySQL
- Unity (2022.3.17f1 기준)

### 📦 설치 방법
```bash
cd node-server
npm install

-- DB 이름
CREATE DATABASE atmdb;

-- 테이블 구조
CREATE TABLE users (
  userName VARCHAR(50) PRIMARY KEY,   -- 로그인 ID
  password VARCHAR(100) NOT NULL,     -- 비밀번호
  name VARCHAR(50),                   -- 사용자 이름
  cash INT,                           -- 현금 보유액
  bankBalance INT                     -- 은행 잔액
);

-- 테스트 데이터 삽입
INSERT INTO users (userName, password, name, cash, bankBalance)
VALUES ('test123', '1234', '테스트유저', 10000, 50000);

API 예시
✅ 회원가입
URL: POST /api/register

Body:

json
{
  "userName": "newUser",
  "password": "abcd1234",
  "name": "홍길동"
}
✅ 로그인
URL: POST /api/login

Body:

json
{
  "userName": "test123",
  "password": "1234"
}

🧩 유니티 연동
Unity에서는 APIManager.cs를 통해 Node.js API와 통신합니다.

사용자 입력에 따라 로그인/회원가입/입출금 요청을 전송하고, 결과를 UI에 반영합니다.