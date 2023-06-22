# coding: UTF-8
from sklearn import datasets
from sklearn import svm
from sklearn import metrics
import matplotlib.pyplot as plt

# 数字データの読み込み
digits = datasets.load_digits()

# 数字データの表示 (一部)
images_and_labels = list(zip(digits.images, digits.target))
for index, (image, label) in enumerate(images_and_labels[:10]):
    plt.subplot(2, 5, index + 1)
    plt.imshow(image, cmap=plt.cm.gray_r, interpolation='nearest')
    plt.axis('off')
    plt.title('Training: %i' % label)
plt.show()

# データ数
data_number = len(digits.data)
# 学習用データ数 (6割のデータを使用、残りの4割は検証用)
sample_number = int(data_number * 6 / 10)
# 検証用データ数 (6割のデータを使用、残りの4割は検証用)
test_number = data_number - sample_number

# サポート ベクター マシーン
clf = svm.SVC(gamma = 0.001, C = 100.0)
# サポート ベクター マシーンによる訓練
clf.fit(digits.data[:sample_number], digits.target[:sample_number])

# 残り4割のデータから、数字を読み取る
# 正解
expected = digits.target[-test_number:]
# 予測
predited = clf.predict(digits.data[-test_number:])
# 正解率
print(metrics.classification_report(expected, predited))
# 誤認識のマトリックス
print(metrics.confusion_matrix(expected, predited))

# 予測と画像の対応 (一部)
images = digits.images[-test_number:]
for i in range(12):
    plt.subplot(3, 4, i + 1)
    plt.axis("off")
    plt.imshow(images[i], cmap=plt.cm.gray_r, interpolation="nearest")
    plt.title("Guess: " + str(predited[i]))
plt.show()
