# GoatGoatGoatGoatGoat
질량을 조작하여 풀어나가는 1인칭 퍼즐 게임.

### 조작 및 설명
플레이어는 사람처럼 두발로 걷는 염소이다.
염소는 WASD키로 움직이고, 스페이스로 점프할 수 있다. 또 E키로 물체를 들 수 있지만, 자신보다 무거운 물체는 들 수 없다.

염소는 초식 동물이기 때문에 초록색인 물체들을 마우스 왼쪽 클릭으로 먹거나 (질량을 빼앗는다), 오른쪽 클릭으로 뱉을 수 있다 (질량을 준다).
초록색인 물체들과 염소가 질량을 서로 주고 받는 것이다.

염소가 노란 보석을 모으면 염소는 게임에서 승리한다.
하지만 염소는 순도 100% 알칼리 금속으로 이루어져 물에 닿으면 폭발하므로, 물에 닿지 않도록 조심해야한다.

염소는 먹거나 뱉어서 무거워질 수도 있고, 가벼워 질수도 있다. 또 동시에 커질 수도 있고, 작아질 수도 있다. 이는 초록색인 모든 물체들도 마찬가지이다. 이를 잘 고려하여 퍼즐을 풀어보자.

### 주로 개발한 것
1. 실시간으로 가변하는 플레이어 크기에 따라 점프 높이, 이동 속도, 들 수 있는 최대 무게가 변하는 1인칭 컨트롤러
1. 물체를 드는 행위, 버튼을 누르는 행위, 언제 먹고 뱉을 수 있는 지 등을 UI와 버튼 이벤트에 따른 피드백으로 유저에게 전달
1. 플레이어의 가변하는 질량과 부피에 따라 상응하여 변하는 점프 높이, 이동 속도, 들수 있는 물체의 최대 무게
1. 퍼즐적 요소, 레벨 디자인
